namespace EducationalPlatform.Application.Helpers;

public static class TokenUtils
{
    private const string Alphabet = "abcdefghijklmnopqrstuvwxyz0123456789";
    private static readonly Random Random = new();

    public static string GenerateToken(int length)
    {
        return GenerateToken(Alphabet, length);
    }

    private static string GenerateToken(string characters, int length)
    {
        return new string(Enumerable
            .Range(0, length)
            .Select(_ => characters[Random.Next() % characters.Length])
            .ToArray());
    }
}