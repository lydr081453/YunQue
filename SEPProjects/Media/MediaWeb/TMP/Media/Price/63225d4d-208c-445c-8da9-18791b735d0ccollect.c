#include <stdio.h> 

#include <stdlib.h> 

#define MAXLEN 80 

#define EXTRA 5 

/* 4个字节留给字段的名字"data", 1个字节留给"=" */ 

#define MAXINPUT MAXLEN+EXTRA+2 

/* 1个字节留给换行符，还有一个留给后面的NULL */ 

#define DATAFILE "/mnt/Nand3/data.txt" 

/* 要被添加数据的文件 */ 


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

printf("<P>表单提交错误"); 

else { 

FILE *f; 

fgets(input, len+1, stdin); 

unencode(input+EXTRA, input+len, data); 

f = fopen(DATAFILE, "a"); 

if(f == NULL) 

printf("<P>对不起，意外错误，不能够保存你的数据 "); 

else 

fputs(data, f); 

fclose(f); 

printf("<P>非常感谢，您的数据已经被保存<BR>%s",data); 

} 

return 0; 

} 
