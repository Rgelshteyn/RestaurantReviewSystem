<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="reservation.aspx.cs" Inherits="ResturantReviewSystemProj3.reservation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="reservation.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <h2>Make a Reservation</h2>
        <div>
            <label for="ddlReservationRestaurant">Select Restaurant:</label>
            <asp:DropDownList ID="ddlReservationRestaurant" runat="server"></asp:DropDownList>
        </div>
        <div>
            <label for="calReservationDate">Date:</label>
            <asp:Calendar ID="calReservationDate" runat="server"></asp:Calendar>
        </div>
        <div>
            <label for="ddlReservationTime">Time:</label>
            <asp:DropDownList ID="ddlReservationTime" runat="server">   
    <asp:ListItem Text="16:00"/>
    <asp:ListItem Text="16:30"/>
    <asp:ListItem Text="17:00"/>
    <asp:ListItem Text="17:30"/>
    <asp:ListItem Text="18:00"/>
    <asp:ListItem Text="18:30"/>
    <asp:ListItem Text="19:00"/>
    <asp:ListItem Text="19:30"/>
    <asp:ListItem Text="20:00"/>
    <asp:ListItem Text="20:30"/>
    <asp:ListItem Text="21:00"/>
</asp:DropDownList>
        </div>
        <div>
            <asp:Button ID="btnSubmitReservation" Text="Make Reservation" runat="server" OnClick="btnSubmitReservation_Click" />
            <asp:Label ID="lblReservationStatus" runat="server"></asp:Label>
        </div>
            <h2>Manage Reservations</h2>

        <asp:UpdatePanel ID="Updatepanel1" runat="server">
            <ContentTemplate>
            <asp:TextBox ID="txtReservationID" runat="server" />
            <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click"/>
            <asp:Label ID="lblDeleteStatus" runat="server" Text=""></asp:Label>

    <asp:GridView ID="gvReservations" runat="server" AutoGenerateColumns="False" DataKeyNames="ReservationID" OnRowEditing="gvReservations_RowEditing" OnRowUpdating="gvReservations_RowUpdating" OnRowCancelingEdit="gvReservations_RowCancelingEdit">
        <Columns>
            <asp:BoundField DataField="ReservationID" HeaderText="Reservation ID" ReadOnly="True" />
            <asp:BoundField DataField="RestaurantName" HeaderText="Restaurant Name" ReadOnly="True" />
            <asp:TemplateField HeaderText="Image">
                <ItemTemplate>
                     <asp:Image ID="imgReservation" runat="server" ImageUrl='<%# Bind("ImageUrl") %>' Width="100" Height="100" />
                </ItemTemplate>
            </asp:TemplateField>
               <asp:TemplateField HeaderText="Reservation Date">
            <ItemTemplate>
                <asp:Label ID="lblReservationDate" runat="server" Text='<%# Bind("ReservationDate") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:Calendar ID="calEditReservationDate" runat="server" SelectedDate='<%# ConvertToDate(Eval("ReservationDate")) %>'></asp:Calendar>
            </EditItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Time">
            <ItemTemplate>
                <asp:Label ID="lblReservationTime" runat="server" Text='<%# Bind("ReservationTime") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:DropDownList ID="ddlEditReservationTime" runat="server">
                   <asp:ListItem Text="16:00"/>
                        <asp:ListItem Text="16:30"/>
                        <asp:ListItem Text="17:00"/>
                        <asp:ListItem Text="17:30"/>
                        <asp:ListItem Text="18:00"/>
                        <asp:ListItem Text="18:30"/>
                        <asp:ListItem Text="19:00"/>
                        <asp:ListItem Text="19:30"/>
                        <asp:ListItem Text="20:00"/>
                        <asp:ListItem Text="20:30"/>
                        <asp:ListItem Text="21:00"/>
                </asp:DropDownList>
            </EditItemTemplate>
        </asp:TemplateField>
            <asp:CommandField ButtonType="Button" ShowEditButton="True" />
            </Columns>
    </asp:GridView>
            </ContentTemplate>
            </asp:UpdatePanel>
            
        <asp:Button ID="btnReturn" runat="server" Text="Return To Dashboard" OnClick="btnReturn_Click" />
    </form>
</body>
</html>
