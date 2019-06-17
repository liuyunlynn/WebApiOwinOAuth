# WebApi Owin OAuth

# Useage
The WebApiOwinOAuth is the server and OwinOauthClient is the client(Windows Form)
So should run the two application at the same time.

Firstly, you should change the baesurl in the client,like this:

private readonly string _baseUrl = "http://localhost:44367/api/";

![image](https://github.com/liuyunlynn/WebApiOwinOAuth/blob/master/1.jpg)  
If you are not authorized, Click 'Get User' button, the result will show Unauthorized  
![image](https://github.com/liuyunlynn/WebApiOwinOAuth/blob/master/2.jpg)  
If you input the incorrect user name and password, the result will display Bad request.  
![image](https://github.com/liuyunlynn/WebApiOwinOAuth/blob/master/3.jpg)  
Also,  Click 'Get User' button, the result will show Unauthorized  
![image](https://github.com/liuyunlynn/WebApiOwinOAuth/blob/master/4.jpg)  
Input correct user name and password, click 'Validate User' buton, the result will display access token  
![image](https://github.com/liuyunlynn/WebApiOwinOAuth/blob/master/4.jpg)  
And Click 'Get User' button, the result will show right result

