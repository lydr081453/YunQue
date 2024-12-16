#include <stdio.h> 

#include <stdlib.h> 

#define MAXLEN 80 

#define EXTRA 5 

/* 4���ֽ������ֶε�����"data", 1���ֽ�����"=" */ 

#define MAXINPUT MAXLEN+EXTRA+2 

/* 1���ֽ��������з�������һ�����������NULL */ 

#define DATAFILE "/mnt/Nand3/data.txt" 

/* Ҫ��������ݵ��ļ� */ 


void unencode(char *src, char *last, char *dest) 

{ 

for(; src != last; src++, dest++) 

if(*src == "+") 

*dest = " "; 

else if(*src == "%") { 

int code; 

if(sscanf(src+1, "%2x", &code) != 1) code = "?"; 

*dest = code; 

src +=2; } 

else 

*dest = *src; 

*dest =" "; 

*++dest = " "; 

} 

int main(void) 

{ 

char *lenstr; 

char input[MAXINPUT], data[MAXINPUT]; 

long len; 

printf("%s%c%c","Content-Type:text/html;charset=gb2312\n\n",13,10); 

printf("<TITLE>Response</TITLE>"); 

lenstr = getenv("CONTENT_LENGTH"); 

if(lenstr == NULL || sscanf(lenstr,"%ld",&len)!=1 || len> MAXLEN) 

printf("<P>���ύ����"); 

else { 

FILE *f; 

fgets(input, len+1, stdin); 

unencode(input+EXTRA, input+len, data); 

f = fopen(DATAFILE, "a"); 

if(f == NULL) 

printf("<P>�Բ���������󣬲��ܹ������������ "); 

else 

fputs(data, f); 

fclose(f); 

printf("<P>�ǳ���л�����������Ѿ�������<BR>%s",data); 

} 

return 0; 

} 
