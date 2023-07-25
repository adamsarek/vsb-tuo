#ifndef __PROGTEST__
#include <iostream>
#include <fstream>
#include <sstream>
#include <cstring>

using namespace std;

#define EASTER_OK                0
#define EASTER_INVALID_FILENAME  1
#define EASTER_INVALID_YEARS     2
#define EASTER_IO_ERROR          3
#endif /* __PROGTEST__ */

int isFileNameCorrect( const char *outFileName )
{
    int iSuffix = strlen(outFileName) - 5;

    if( iSuffix > 0 )
    {
        for( unsigned int i = 0; i < strlen(outFileName); i++ )
        {
            if( outFileName[i] == ' ' )
            {
                return 0;
                break;
            }

            if( ( outFileName[i] >= '0'&& outFileName[i] >= '9' )
                || ( outFileName[i] >= 'a' && outFileName[i] <= 'z' )
                || ( outFileName[i] >= 'A' && outFileName[i] <= 'Z' )
                || outFileName[i] == '.' || outFileName[i] == '\\' || outFileName[i] == '/' )
                continue;
        }

        string sSuffix = ".html";

        for( int i = 0; i < 5; i++ )
        {
            if( outFileName[iSuffix + i] == sSuffix[i] )
            {
                if( i == 4 )
                    return 1;

                continue;
            }
            else return 0;
        }
    }

    return 0;
}

int getSumOfYears( stringstream &ssYears )
{
    int iYear, iNumYears = 0, iTemp;
    char cChar;

    while( ssYears >> iYear )
    {
        if( iYear <= 1582 || iYear >= 2200 )
        {
            iNumYears = 0;
            break;
        }

        cChar = ssYears.get();
        iNumYears++;

        if( cChar == '-' )
        {
            ssYears >> iTemp;

            if( iTemp < iYear || iTemp <= 1582 || iTemp >= 2200 )
            {
                iNumYears = 0;
                break;
            }

            iNumYears += iTemp - iYear;
            cChar = ssYears.get();

            if( cChar == '-' )
            {
                iNumYears = 0;
                break;
            }
        }
    }

    ssYears.clear();
    ssYears.seekg( 0, ios::beg );

    return iNumYears;
}

void getYearsFromSStream( stringstream &ssYears, int *iYears, const int iNumYears )
{
    int iTemp, iYear;
    char cChar;

    for( int i = 0; i < iNumYears; i++ )
    {
        ssYears >> iYears[i];

        cChar = ssYears.get();

        if( cChar == '-' )
        {
            ssYears >> iYear;
            iTemp = iYear - iYears[i];

            for( int j = 1; j <= iTemp; j++ )
            {
                i++;
                iYears[i] = iYear + j - iTemp;
            }

            cChar = ssYears.get();
        }
    }
}

int insertToFile( int *iYears, const int  iNumYears, const char *outFileName )
{
    ofstream outFile( outFileName, ios::out );

    if( outFile.is_open() )
    {
        outFile << "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\">" << endl;
        outFile << "<html>" << endl << "<head>" << endl;
        outFile << "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">" << endl;
        outFile << "<title>C++</title>" << endl;
        outFile << "</head>" << endl << "<body>" << endl;
        outFile << "<table width=\"300\">" << endl;
        outFile << "<tr><th width=\"99\">den</th><th width=\"99\">mesic</th><th width=\"99\">rok</th></tr>" << endl;

        int Y, H, L, N, P;
        for( int i = 0; i < iNumYears; i++ )
        {
            Y = iYears[i];

            H = (19*(Y % 19) + (Y / 100) - ((Y / 100) / 4) - (((Y / 100) - (((Y / 100) + 8) / 25) + 1) / 3) + 15) % 30;
            L = (32 + 2*((Y / 100) % 4) + 2*((Y % 100) / 4) - H - ((Y % 100) % 4)) % 7;
            N = (H + L - 7*(((Y % 19) + 11*H + 22*L) / 451) + 114) / 31;
            P = (H + L - 7*(((Y % 19) + 11*H + 22*L) / 451) + 114) % 31;

            string sMonth = (N == 3) ? "brezen" : "duben";

            outFile << "<tr>";
            outFile << "<td>" << P + 1 << "</td>";
            outFile << "<td>" << sMonth << "</td>";
            outFile << "<td>" << Y << "</td>";
            outFile << "</tr>";

            outFile << endl;
        }

        outFile << "</table>" << endl << "</body>" << endl << "</html>";
        outFile.close();

        delete[] iYears;
        iYears = NULL;

        return EASTER_OK;
    }

    outFile.close();

    delete[] iYears;
    iYears = NULL;

    return EASTER_IO_ERROR;
}

int easterReport( const char * years, const char * outFileName )
{
    if( isFileNameCorrect( outFileName ) )
    {
        if( strlen(years) != 0 )
        {
            stringstream ssYears;
            int *iYears;

            for( unsigned int i = 0; i < strlen(years); i++ )
            {
                if( years[i] == ' ' )
                    continue;

                if( ( years[i] >= '0' && years[i]  <= '9' ) || years[i] == ',' || years[i] == '-' )
                {
                    ssYears << years[i];
                    continue;
                }

                return EASTER_INVALID_YEARS;
            }

            cout << ssYears.str() << endl;

            int iNumYears = getSumOfYears( ssYears );

            if( iNumYears == 0 )
                return EASTER_INVALID_YEARS;

            iYears = new int[iNumYears];
            getYearsFromSStream( ssYears, iYears, iNumYears );

            return insertToFile( iYears, iNumYears, outFileName );
        }
        else return EASTER_INVALID_YEARS;
    }
    else return EASTER_INVALID_FILENAME;
}

#ifndef __PROGTEST__
int main()
{
    cout << easterReport( "2000-2010-2020", "outfile.html" );
    return 0;
}
#endif /* __PROGTEST__ */
