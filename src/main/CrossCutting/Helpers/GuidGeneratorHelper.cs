namespace CrossCutting.Helpers;

public static class GuidGeneratorHelper
{
    public static Guid Generate()
    {
        return Guid.CreateVersion7();
    }
}
