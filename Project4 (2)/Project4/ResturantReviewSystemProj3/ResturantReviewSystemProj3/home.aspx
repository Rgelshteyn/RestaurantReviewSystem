<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="ResturantReviewSystemProj3.home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="home.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
       <h2>Welcome to the Restaurant Review System!</h2>

        <asp:Label ID="lblErrorMessage" runat="server"/>

        <asp:Label ID="lblUsername" runat="server" Text="Username:" />
        <asp:TextBox ID="txtUsername" runat="server" />

        <asp:Label ID="lblPassword" runat="server" Text="Password:" />
        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" />

        <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="LoginButtonClicked" />
        <asp:Button ID="btnCreateAcount" runat="server" Text="Create New Account!" OnClick="btnCreateAcount_Click"  />
         <asp:Button ID="btnGuest" runat="server" Text="Continue As Guest" OnClick="btnGuest_Click" />

        <hr/>

    </form>
</body>
</html>
