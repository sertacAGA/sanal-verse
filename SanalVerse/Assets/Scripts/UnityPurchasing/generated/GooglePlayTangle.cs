// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("D2YElAuQX2VefgUT6nmjAShcWGBR0tzT41HS2dFR0tLTXfvuIeZxmv+64gilfPklQq/dEqSkkH+Ll0iihw1AxqNkEMBtjsZORV7q3O6fMxrAYlHLhEnTJcRsuK53E3977Qqa6GX43HgbNJqvG6dNafLs+CKXxkQrbIbABfaNeVBWAnuxVjeIONZjbkVYoMxY1SXfUW29hb3ZDJU0bWq48Qy6Cw/94N1WFf2Ng27vHuTmOg8G6iuzlnyqM6MHt58NPHCpn3puiyZkLlOGXPTKRhwrRASUQDGiodDO2Sspyn21VRwg3Tb99jg0FBF0dLU241HS8ePe1dr5VZtVJN7S0tLW09A6cKdSn31Z0TAT4Ae/UuR8QnnQwOYG44zvu0G0/NHQ0tPS");
        private static int[] order = new int[] { 12,1,5,8,11,9,11,10,9,11,11,13,12,13,14 };
        private static int key = 211;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
