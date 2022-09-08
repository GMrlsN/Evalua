#include <iostream>
#include <stdio.h>
#include <conio.h>
float resultado;
float area, radio, pi;
//int a, b, c,a,i;
//char opcion;
///float radio, altura, x;
// Este programa calcula el volumen de un cilindro.
void main()
{   
    /*printf("hola mundo\tacabo de insertar un tabulador\ny ahora una nueva linea\n");
    printf("Resultado = ");
    x = 10;
    //a = (3 + 5)*8 - (x - 4)/2;
    for(i = 0; i < 10; i++){
        //printf( (3 + 5)*8 - (x - 4)/2);
    }*/
    printf("Radio = ");
    scanf("%d",&radio);
    pi = 3.141592653589793;
    area =  pi * (radio * radio);
    printf("Area = ");
    printf(area);
}