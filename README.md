# DemoReCapcha2
Solving reCAPTCHA 2 using Xevil installed on a Windows server.

Google captcha bypass demo https://www.google.com/recaptcha/api2/demo
Xevil (with license) is installed on the remote Windows server. It does not provide documentation for its APIs.
1. Get the page code with captcha.
2. We take the key we need from the html code
3. We send a request to our server. We are waiting for the result
4. We form a POST request with the result of the captcha.
