using AffairList.Constants;
using AffairList.Services.Models;
using System.Text.Json;

namespace AffairList.Services.Providers
{
    public static class AffairsProvider
    {
        private static SemaphoreSlim _listFileLocker;

        static AffairsProvider(){
            _listFileLocker = new SemaphoreSlim(1, 1);
        }
        
        public static async Task<AffairsCollection> GetAffairsAsync(string profile)
        {
            await _listFileLocker.WaitAsync();
            try
            {
                using (FileStream fileStream = new FileStream(profile, FileMode.Open))
                {
                    return await JsonSerializer.DeserializeAsync<AffairsCollection>(fileStream);
                }
            }
            finally
            {
                _listFileLocker.Release();
            }
        }
        public static async Task SaveAffairsAsync(string profile, AffairsCollection affairsCollection)
        {
            await _listFileLocker.WaitAsync();
            try
            {
                await File.WriteAllTextAsync(profile, JsonSerializer.Serialize(affairsCollection));
            }
            finally
            {
                _listFileLocker.Release();
            }
        }
        public static async Task<AffairsCollection> GetAffairsLegacyAsync(string profile)
        {
            await _listFileLocker.WaitAsync();
            try
            {
                string[] affairs = await File.ReadAllLinesAsync(profile);
                AffairsCollection affairsCollection = new AffairsCollection();
                for (int i = 0; i < affairs.Length; i++)
                {
                    Affair affair = new Affair(affairs[i]);
                    if (affair.InnerText.Length > AffairConstants.DeadlineDateNTagLength 
                        && affair.InnerText.Contains(AffairConstants.DeadlineTag))
                    {
                        DateOnly deadline;
                        bool parseSucceded = DateOnly
                            .TryParse(affairs[i].AsSpan(AffairConstants.DeadlineTag.Length, AffairConstants.DeadlineDateLength), out deadline);
                        if (parseSucceded)
                        {
                            affair.Deadline = deadline;
                            affair.InnerText = affair.InnerText.Substring(AffairConstants.DeadlineDateNTagLength - 1);
                        }
                    }
                    if (affair.InnerText.Length > AffairConstants.PriorityTag.Length
                        && affair.InnerText.EndsWith(AffairConstants.PriorityTag))
                    {
                        affair.IsPrioritized = true;
                        affair.InnerText = affair.InnerText.Substring(0, affair.InnerText.Length - AffairConstants.PriorityTag.Length);
                    }

                    affairsCollection.Affairs.Add(affair);
                }
                return affairsCollection;
            }
            finally
            {
                _listFileLocker.Release();
            }
        }
    }
}
