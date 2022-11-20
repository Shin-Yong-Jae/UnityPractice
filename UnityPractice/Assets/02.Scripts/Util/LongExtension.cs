using UnityEngine;

public static class LongExtension
{
    private const long KYUNG = 10000000000000000;
    private const long JO = 1000000000000;
    private const long UK = 1000000000;
    private const long MAN = 10000;

    private const long Q = 1000000000000000;
    private const long T = 1000000000000;
    private const long B = 1000000000;
    private const long M = 1000000;
    private const long K = 1000;

    public static string ToText(this int l)
    {
        return ((long)l).ToText();
    }

    public static string ToText(this long l)
    {
        if (Application.systemLanguage ==  SystemLanguage.Korean)
        {
            if (l >= KYUNG)
            {
                int v1 = (int)(l / KYUNG);
                int v2 = (int)(l / JO % 10000);

                string result = v1 + "경";
                if (v2 > 0)
                    result += v2 + "조";

                return result;
            }

            if (l >= JO)
            {
                int v1 = (int)(l / JO);
                int v2 = (int)(l / UK % 10000);

                string result = v1 + "조";
                if (v2 > 0)
                    result += v2 + "억";

                return result;
            }

            if (l >= UK)
            {
                int v1 = (int)(l / UK);
                int v2 = (int)(l / MAN % 10000);

                string result = v1 + "억";
                if (v2 > 0)
                    result += v2 + "만";

                return result;
            }

            if (l >= MAN)
            {
                int v1 = (int)(l / MAN);
                int v2 = (int)(l % 10000);

                string result = v1 + "만";
                if (v2 > 0)
                    result += v2;

                return result;
            }

            return l.ToString();
        }
        else
        {
            if (l >= Q)
            {
                int v1 = (int)(l / Q);
                int v2 = (int)(l / T % 1000);

                string result = v1 + "Q";
                if (v2 > 0)
                    result += v2 + "T";

                return result;
            }

            if (l >= T)
            {
                int v1 = (int)(l / T);
                int v2 = (int)(l / B % 1000);

                string result = v1 + "T";
                if (v2 > 0)
                    result += v2 + "B";

                return result;
            }

            if (l >= B)
            {
                int v1 = (int)(l / B);
                int v2 = (int)(l / M % 1000);

                string result = v1 + "B";
                if (v2 > 0)
                    result += v2 + "M";

                return result;
            }

            if (l >= M)
            {
                int v1 = (int)(l / M);
                int v2 = (int)(l / K % 1000);

                string result = v1 + "M";
                if (v2 > 0)
                    result += v2 + "K";

                return result;
            }

            return l.ToString();
        }
    }
}
