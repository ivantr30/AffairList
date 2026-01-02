namespace AffairList
{
    public static class PointExtension
    {
        /// <summary>
        /// Adds deltaX to the point's x
        /// </summary>
        /// <param name="point"></param>
        /// <param name="deltaX"></param>
        public static void ChangeXPos(this Point point, int deltaX)
        {
            point.X += deltaX;
        }
        /// <summary>
        /// Adds deltaY to the point's y
        /// </summary>
        /// <param name="point"></param>
        /// <param name="deltaY"></param>
        public static void ChangeYPos(this Point point, int deltaY)
        {
            point.Y += deltaY;
        }
    }
}
