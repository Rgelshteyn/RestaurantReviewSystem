<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="createAccount.aspx.cs" Inherits="ResturantReviewSystemProj3.createAccount" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="account.css" rel="stylesheet" />
</head>
<body>
    <<form id="form1" runat="server">
        <div class="account-form">
            <h2>Create New Account</h2>
            <br />

            <label for="txtUsername">Username:</label>
            <asp:TextBox ID="txtUsername" runat="server" placeholder="Enter username"></asp:TextBox>
            <br /><br />

            <label for="txtPassword">Password:</label>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Enter password"></asp:TextBox>
            <br /><br />

            <label for="ddlUserType">Account Type:</label>
            <asp:DropDownList ID="ddlUserType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlUserType_SelectedIndexChanged">
                <asp:ListItem Value="Reviewer">Reviewer</asp:ListItem>
                <asp:ListItem Value="Restaurant Representative">Restaurant Representative</asp:ListItem>
            </asp:DropDownList>
            <br /><br />

            
            <asp:Panel ID="pnlRepresentativeDetails" runat="server" Visible="False">
    <asp:TextBox ID="txtRepresentativeName" runat="server" placeholder="Name"></asp:TextBox>
    <asp:TextBox ID="txtPhoneNumber" runat="server" placeholder="Phone Number"></asp:TextBox>
    <asp:TextBox ID="txtEmail" runat="server" placeholder="Email"></asp:TextBox>
</asp:Panel>

            <asp:Button ID="btnSubmit" runat="server" Text="Create Account" OnClick="btnSubmit_Click" />
            <br /><br />
            <asp:Button ID="btnLogin" runat="server" Text="Go Back to Login" OnClick="btnLogin_Click"/>
            <br /><br />
            <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label>
            <asp:Label ID="lblSuccess" runat="server" Text="" ForeColor="Green"></asp:Label>

        </div>
    </form>
</body>
</html>
