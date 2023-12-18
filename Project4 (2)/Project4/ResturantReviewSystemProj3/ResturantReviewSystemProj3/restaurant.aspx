<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="restaurant.aspx.cs" Inherits="ResturantReviewSystemProj3.restuarant" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="restaurant.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <h2>Add Restaurant</h2>
        
        <div>
            <label for="txtRestaurantName">Restaurant Name:</label>
            <asp:TextBox ID="txtRestaurantName" runat="server"></asp:TextBox>
           
        </div>

        <div>
            <label for="ddlCategory">Category:</label>
            <asp:DropDownList ID="ddlCategory" runat="server">
            </asp:DropDownList>
        </div>


        <div>
            <label for="txtAddress">Address:</label>
            <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine"></asp:TextBox>
        </div>

        <div id="representativeAssignment">
            <label for="ddlRepresentatives">Assign Representative:</label>
            <asp:DropDownList ID="ddlRepresentatives" runat="server">
               
            </asp:DropDownList>
        </div>

        <div>
            <label>Image Url</label>
            <asp:TextBox ID="txtImageUrl" runat="server"></asp:TextBox>
        </div>

        <div>
            <asp:Button ID="btnSubmit" runat="server" Text="Add Restaurant" OnClick="btnSubmit_Click" style="height: 48px" />
            
        </div>

        <asp:TextBox ID="txtDeleteRestaurantID" runat="server" placeholder="Enter Restaurant ID to delete"></asp:TextBox>
        <asp:Button ID="btnDeleteRestaurant" runat="server" Text="Delete Restaurant" OnClick="btnDeleteRestaurant_Click" />
        <asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
        <div>
        <asp:CheckBoxList ID="cblCategories" runat="server" AutoPostBack="false"></asp:CheckBoxList>
            <asp:Button ID="btnSearchcbl" runat="server" Text="Search By Category" OnClick="btnSearch_Click" />
        <asp:Button ID="btnSearch" runat="server" Text="Search By Restaurant Name =>>" OnClick="btnSearchGrid_Click" />
            <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
        <asp:Button ID="btnResetGV" runat="server" Text="Reset Search" OnClick="btnResetGV_Click" />
        </div>
        <div id="gv-grid">
        <asp:GridView ID="gvRestaurants" runat="server" AutoGenerateColumns="False" DataKeyNames="RestaurantID" OnRowDataBound="gvRestaurants_RowDataBound" OnRowUpdating="gvRestaurants_RowUpdating" OnRowCancelingEdit="gvRestaurants_RowCancelingEdit" OnRowEditing ="gvRestaurants_RowEditing">
    <Columns>
        <asp:BoundField DataField="RestaurantID" HeaderText="Restaurant ID" ReadOnly="True" />
         <asp:TemplateField HeaderText="Name">
            <ItemTemplate>
                <asp:Label ID="lblName" runat="server" Text='<%# Bind("name") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="TextBoxName" runat="server" Text='<%# Bind("name") %>'></asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Address">
            <ItemTemplate>
                <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("address") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtAddressEdit" runat="server" Text='<%# Eval("address") %>'></asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Category">
            <ItemTemplate>
                <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("CategoryName") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:DropDownList ID="ddlCategoryEdit" runat="server"></asp:DropDownList>
            </EditItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Representative">
            <ItemTemplate>
                <asp:Label ID="lblRepresentative" runat="server" Text='<%# Eval("RepresentativeName") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:DropDownList ID="ddlRepresentativesEdit" runat="server"></asp:DropDownList>
            </EditItemTemplate>
        </asp:TemplateField>

        <asp:BoundField DataField="PhoneNumber" HeaderText="Phone Number" ReadOnly="true" />
        <asp:BoundField DataField="Email" HeaderText="Email" ReadOnly="true" />
        <asp:TemplateField HeaderText="Image">
            <ItemTemplate>
                <asp:Image ID="imgRestaurant" runat="server" ImageUrl='<%# Bind("ImageUrl") %>' Width="100" Height="100" />
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="TextBoxImageUrl" runat="server" Text='<%# Bind("ImageUrl") %>'></asp:TextBox>
            </EditItemTemplate>
       </asp:TemplateField>
        <asp:CommandField ShowEditButton="True" />
    </Columns>
</asp:GridView>
           
        </div>
         </ContentTemplate>
 </asp:UpdatePanel>
        <div>
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
        </div>
          <asp:Button ID="btnBack" runat="server" Text="Back To Dashboard" OnClick="btnBack_Click" />
    </form>
</body>
</html>
