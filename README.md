# FoonkieMonkeyTI
## _Prueba Backend_

**PASOS PARA REVISAR CORRECTAMENTE LA PRUEBA**
1. Clonar este repositorio
2. Crear una base de datos en SQL Server
3. Obtener la cadena de conexión de esta nueva base de datos
4. Esa cadena de conexión debe ser reemplazada en _'appsettings.json'_ como podemos ver a continuación:
   
 ![image](https://github.com/Estefa28/FoonkieMonkeyTI/assets/123497973/8c5f0e74-8cda-4f89-b8f1-92e97a5c6138)

5. Realizar actualización de la base de datos en Entity Framework de la siguiente manera: 
  * Abrir la consola de administrador de paquetes en Visual Studio
  * Seleccionar el proyecto **FM.EntityFramework** en esta consola
  * Correr el siguiente comando: Update-Database
  
![image](https://github.com/Estefa28/FoonkieMonkeyTI/assets/123497973/4401c779-25ea-460c-9987-c4ed46c05718)

6. Esta es la información necesaria para llevar a cabo la Autorización en la aplicación.

   *Los Guids son:
   
   "ClientSecret": "50dbd733-55a2-426f-ba1e-a940cb6b8d24"
   
   "ClientId": "3bab2526-c831-429f-964c-78ca2fd90c62"

   ![image](https://github.com/Estefa28/FoonkieMonkeyTI/assets/123497973/e2d8b8de-0971-4b92-a982-943ee4204f3f)

   **Nota:** Estos deben ser enviados en los Headers de la petición
   
   ![image](https://github.com/Estefa28/FoonkieMonkeyTI/assets/123497973/be7b17ce-23f9-41da-ad0f-4305c41885b6)

8. Correr el Proyecto.

   **Nota** Si se desea cambiar la frecuencia con la que se ejecuta la actualización de la base de datos con la API Externa, se debe cambiar el valor en el appsettings.json como lo muestra la imagen. La duración es en Milisegundos.

   ![image](https://github.com/Estefa28/FoonkieMonkeyTI/assets/123497973/2c11f9a6-a250-410b-a610-f56905542e9a)
