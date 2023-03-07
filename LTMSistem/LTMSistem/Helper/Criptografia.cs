namespace LTMSistem.Helper
{
    public static class Criptografia
    {
        public static string GerarHash(this string valor)
        {
            valor= BCrypt.Net.BCrypt.HashPassword(valor);
            return valor;
        }
    }
}
