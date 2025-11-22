using System.Runtime.CompilerServices;

namespace CrossCutting.Validators;

public static class CpfValidator
{
    public static bool Validate(string? sourceCpf)
    {
        if (string.IsNullOrWhiteSpace(sourceCpf))
            return false;

        string clearCpf = sourceCpf.Trim();
        clearCpf = clearCpf.Replace("-", "", StringComparison.OrdinalIgnoreCase);
        clearCpf = clearCpf.Replace(".", "", StringComparison.OrdinalIgnoreCase);

        if (clearCpf.Length != 11)
        {
            return false;
        }

        int totalDigito1 = 0;
        int totalDigito2 = 0;

        if (clearCpf.Equals("00000000000", StringComparison.OrdinalIgnoreCase) ||
            clearCpf.Equals("11111111111", StringComparison.OrdinalIgnoreCase) ||
            clearCpf.Equals("22222222222", StringComparison.OrdinalIgnoreCase) ||
            clearCpf.Equals("33333333333", StringComparison.OrdinalIgnoreCase) ||
            clearCpf.Equals("44444444444", StringComparison.OrdinalIgnoreCase) ||
            clearCpf.Equals("55555555555", StringComparison.OrdinalIgnoreCase) ||
            clearCpf.Equals("66666666666", StringComparison.OrdinalIgnoreCase) ||
            clearCpf.Equals("77777777777", StringComparison.OrdinalIgnoreCase) ||
            clearCpf.Equals("88888888888", StringComparison.OrdinalIgnoreCase) ||
            clearCpf.Equals("99999999999", StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        if (clearCpf.Any(c => !char.IsNumber(c)))
        {
            return false;
        }

        for (int posicao = 0; posicao < clearCpf.Length - 2; posicao++)
        {
            totalDigito1 += ObterDigito(clearCpf, posicao) * (10 - posicao);
            totalDigito2 += ObterDigito(clearCpf, posicao) * (11 - posicao);
        }

        int modI = totalDigito1 % 11;
        modI = modI < 2 ? 0 : 11 - modI;

        if (ObterDigito(clearCpf, 9) != modI)
        {
            return false;
        }

        totalDigito2 += modI * 2;
        int mod11 = totalDigito2 % 11;
        mod11 = mod11 < 2 ? 0 : 11 - mod11;

        return ObterDigito(clearCpf, 10) == mod11;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int ObterDigito(
        string value,
        int pos
    ) => value[pos] - '0';
}
