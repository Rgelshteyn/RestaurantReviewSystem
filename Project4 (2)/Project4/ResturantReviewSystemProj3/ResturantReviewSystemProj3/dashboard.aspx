<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dashboard.aspx.cs" Inherits="ResturantReviewSystemProj3.dashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="dashboard.css" rel="stylesheet" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Button ID="btnBack" runat="server" Text="Return to Login" OnClick="btnBack_Click" />
        <asp:Button ID="btnManageRestaurants" runat="server" Text="Manage Restaurants" OnClick="btnManageRestaurants_Click" />
        <asp:Button ID="btnWriteReview" runat="server" Text="Write/View Reviews" OnClick="btnWriteReview_Click" />
        <asp:Button ID="btnMakeReservation" runat="server" Text="Make a Reservation" OnClick="btnMakeReservation_Click" />
    </form>
</body>
</html>
