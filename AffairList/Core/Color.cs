namespace AffairList.Core;

public struct Color(byte r, byte g, byte b, byte a) : IEquatable<Color>
{
    public byte R { get; set; } = r;
    public byte G { get; set; } = g;
    public byte B { get; set; } = b;
    public byte A { get; set; } = a;

    public Color() : this(0, 0, 0, 255) { }
    public Color(byte grey) : this(grey, grey, grey, 255) { }
    public Color(byte r, byte g, byte b) : this(r, g, b, 255) { }

    public static readonly Color Empty = new(0, 0, 0, 0);
    public static readonly Color White = new(255);
    public static readonly Color Gray = new(78);
    public static readonly Color Black = new();
    internal static readonly Color Indigo = new(75, 0, 130);
    internal static readonly Color MediumSpringGreen = new(0, 250, 154);

    public static bool operator ==(Color color1, Color color2)
        => color1.Equals(color2);
    public static bool operator !=(Color color1, Color color2)
        => !color1.Equals(color2);

    public readonly bool Equals(Color other)
        => R == other.R &&
           G == other.G &&
           B == other.B &&
           A == other.A;

    public readonly override bool Equals(object? other)
        => other is Color otherColor && Equals(otherColor);

    public readonly override int GetHashCode()
        => HashCode.Combine(R, G, B, A);

    public readonly override string ToString()
        => $"({R}, {G}, {B}, {A})";

    public static implicit operator Color(System.Drawing.Color color)
        => new(color.R, color.G, color.B, color.A);

    public static explicit operator System.Drawing.Color(Color color)
        => System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
}
