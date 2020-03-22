using System;

namespace Application
{
    internal class SimpleSubstitutionCipher
    {
        internal static string Encrypt( string text, int num )
        {
            string result = string.Empty;

            for ( int i = 0; i < text.ToLower( ).Length; i++ )
            {
                char chartext = text[i];

                if ( char.IsLetter( chartext ) )
                {
                    if ( ( chartext + num ) > 122 )
                    {
                        result += Convert.ToChar( ( chartext + num ) - 26 );
                    }
                    else
                    {
                        result += Convert.ToChar( chartext + num );
                    }
                }
                else
                {
                    result += chartext;
                }
            }

            return result;
        }

        internal static string Decrypt( string text, int num )
        {
            string result = string.Empty;

            for ( int i = 0; i < text.ToLower( ).Length; i++ )
            {
                char chartext = text[i];

                if ( char.IsLetter( chartext ) )
                {
                    if ( ( chartext - num ) < 97 )
                    {
                        result += Convert.ToChar( ( chartext - num ) + 26 );
                    }
                    else
                    {
                        result += Convert.ToChar( chartext - num );
                    }
                }
                else
                {
                    result += chartext;
                }
            }

            return result;
        }
    }
}
