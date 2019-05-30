Besides providing exceptional transportation services, Cabify also runs a physical store which sells 3 products:

```
Code         | Name                |  Price
-------------------------------------------------
VOUCHER      | Cabify Voucher      |   5.00€
TSHIRT       | Cabify T-Shirt      |  20.00€
MUG          | Cabify Coffee Mug   |   7.50€
```

Various departments have insisted on the following discounts:

 * The marketing department thinks a buy 2 get 1 free promotion will work best (buy two of the same product, get one free), and would like this to only apply to `VOUCHER` items.

 * The CFO insists that the best way to increase sales is with discounts on bulk purchases (buying x or more of a product, the price of that product is reduced), and requests that if you buy 3 or more `TSHIRT` items, the price per unit should be 19.00€.

This set of rules to apply may change quite frequently in the future.

Your task is to implement a checkout system for this store.

The system should have differentiated client and server components that communicate over the network.

The server should expose the following independent operations:

- Create a new checkout basket
- Add a product to a basket
- Get the total amount in a basket
- Remove the basket

The server must support concurrent invocations of those operations: any of them may be invoked at any time, while other operations are still being performed, even for the same basket.

The client must connect user input with those operations via the protocol exposed by the server.

We don't have any DBAs at Cabify, so the service shouldn't use any external databases of any kind.

Implement a checkout service and its client that fulfils these requirements.

Examples:

    Items: VOUCHER, TSHIRT, MUG
    Total: 32.50€

    Items: VOUCHER, TSHIRT, VOUCHER
    Total: 25.00€

    Items: TSHIRT, TSHIRT, TSHIRT, VOUCHER, TSHIRT
    Total: 81.00€

    Items: VOUCHER, TSHIRT, VOUCHER, VOUCHER, MUG, TSHIRT, TSHIRT
    Total: 74.50€

**The code should:**
- Build and execute in a Unix operating system.
- Be written as production-ready code. You will write production code.
- Be easy to grow and easy to add new functionality.
- Have notes attached, explaining the solution and why certain things are included and others are left out.
- It must not contain executable or object files. Just source files, documentation and data files are allowed.


**How to execute in Unix-Based:**

- Install .NET Core SDK

Linux: https://dotnet.microsoft.com/download/linux-package-manager/debian9/sdk-current

MacOS: https://dotnet.microsoft.com/download/thank-you/dotnet-sdk-2.2.107-macos-x64-installer

- Edit Checkout.API.csproj file, updating RuntimeIdentifiers tag with your distribution

Ex: <RuntimeIdentifiers>win10-x64;osx.10.12-x64;debian.8-x64</RuntimeIdentifiers>

This configurations runs on Windows 10 64 bit, osx 10.12 64 bit and Debian 8 64 bit

- After installing the SDK, open the CLI (Command Line Interface)
https://docs.microsoft.com/pt-br/dotnet/core/tools/index?tabs=netcore2x

- Browse to the directory that contains CheckoutApi.sln file
- Execute the commands:
dotnet restore
dotnet build
dotnet run

- The console must show that the server is running and listening at "http://127.0.0.1:8080"

- Use a HTTP Client (like Postman) in order to call the API

Executions:

- [POST] Create Basket: http://localhost:8080/api/basket/create
Response: StatusCode 200, {"basketId":"480bf32f-c6d5-4928-a007-5789bbb695b8"}

- [PUT] Add Items to Basket: http://localhost:8080/api/basket/add/<basketId>
Request Body JSON (1 object per request): 
		{
			"code" : "MUG",
			"name" : "Cabify Coffee Mug",
			"price" : 7.50,
			"quantity" : 5
		}

Response: StatusCode 200, "Added 5 Cabify Coffee Mug to the basket"

- [DELETE] Delete Basket: http://localhost:8080/api/basket/delete/<basketId>

Response: StatusCode 200, "Basket ce3d522d-fde5-4f12-a8c5-fd72b18e5718 deleted successfully"

- [GET] Get Basket: http://localhost:8080/api/basket/get/<basketId>

Response JSON:
		{
			"Items" : "VOUCHER, TSHIRT, VOUCHER, VOUCHER, MUG, TSHIRT, TSHIRT",
			"Total": "74.50€"
		}