<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="reviews.aspx.cs" Inherits="ResturantReviewSystemProj3.reviews" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="reviews.css" rel="stylesheet" />
    
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
       <h2>Add Review</h2>
        <div>
            Select Restaurant: 
            <asp:DropDownList ID="ddlRestaurants" runat="server"></asp:DropDownList>
        </div>

        <div>
            Comments: 
            <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Rows="5"></asp:TextBox>
        </div>
        
        <div>
             Food Quality Rating (1 = Very Poor, 5 = Excellent): 
            <asp:DropDownList ID="ddlFoodQuality" runat="server">
                <asp:ListItem>1</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
                <asp:ListItem>3</asp:ListItem>
                <asp:ListItem>4</asp:ListItem>
                <asp:ListItem>5</asp:ListItem>
            </asp:DropDownList>
        </div>
        
        <div>
             Service Rating (1 = Very Poor, 5 = Excellent):  
            <asp:DropDownList ID="ddlServiceRating" runat="server">
                <asp:ListItem>1</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
                <asp:ListItem>3</asp:ListItem>
                <asp:ListItem>4</asp:ListItem>
                <asp:ListItem>5</asp:ListItem>
            </asp:DropDownList>
        </div>
        
        <div>
            Atmosphere Rating (1 = Very Poor, 5 = Excellent): 
            <asp:DropDownList ID="ddlAtmosphereRating" runat="server">
                <asp:ListItem>1</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
                <asp:ListItem>3</asp:ListItem>
                <asp:ListItem>4</asp:ListItem>
                <asp:ListItem>5</asp:ListItem>
            </asp:DropDownList>
        </div>
        
        <div>
            Price Level Rating (1 = Very Poor, 5 = Excellent): 
            <asp:DropDownList ID="ddlPriceLevelRating" runat="server">
                <asp:ListItem>1</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
                <asp:ListItem>3</asp:ListItem>
                <asp:ListItem>4</asp:ListItem>
                <asp:ListItem>5</asp:ListItem>
            </asp:DropDownList>
        </div>
        
        <div>
            <asp:Button ID="btnSubmitReview" Text="Submit Review" runat="server" OnClick="btnSubmitReview_Click" />
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </div>

        <div>
            <asp:Label ID="lblredirect" Text="If the restaurant is not listed add it here!" runat="server"></asp:Label>
            <asp:Button ID="btnRestaurantPage" Text="Add Restaurant" runat="server" OnClick="btnRestaurantPage_Click"/>
        </div>

        <div>
            <asp:Label ID="lblSearch" Text="Delete Review By ID" runat="server"></asp:Label>
            <asp:TextBox ID="txtDeleteReview" runat="server"></asp:TextBox>
            <asp:Button ID="btnDelete" Text="Delete Review" runat="server" OnClick="btnDelete_Click"/>
        </div>
         <asp:UpdatePanel ID="UpdatePanel2" runat="server">
     <ContentTemplate>
        <div>
            <asp:Label ID="lblSearchCat" Text="Search By Catagory" runat="server"></asp:Label>
            <asp:CheckBoxList ID="cblCategories" runat="server" AutoPostBack="false"></asp:CheckBoxList>
            <asp:Button ID="btnSearchCat" runat="server" Text="Search" OnClick="btnSearchCat_Click" />
        </div>
        
     <div>
    <label for="txtSearch">Search By Restaurant Name:</label>
    <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
    <asp:Button ID="btnSearchGrid" runat="server" Text="Search" OnClick="btnSearchGrid_Click" />
    <asp:Button ID="btnResetSearch" runat="server" Text="Reset Search" OnClick="btnResetSearch_Click" />
       </div>
        <h2>Restaurant Reviews</h2>
        <div id="gv-grid">
    <asp:GridView ID="gvReviews" runat="server" AutoGenerateColumns="False" DataKeyNames="ReviewID" OnRowCancelingEdit="gvReviews_RowCancelingEdit" OnRowEditing="gvReviews_RowEditing" OnRowUpdating="gvReviews_RowUpdating">
    <Columns>
        <asp:BoundField DataField="ReviewID" HeaderText="Review ID" ReadOnly="True" />

        <asp:BoundField DataField="RestaurantName" HeaderText="Restaurant Name" ReadOnly="True" />

        <asp:BoundField DataField="CategoryName" HeaderText="Category" ReadOnly="true" />

       <asp:TemplateField HeaderText="Image">
    <ItemTemplate>
        <asp:Image ID="imgReview" runat="server" ImageUrl='<%# Bind("ImageUrl") %>' Width="100" Height="100" />
    </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Food Quality">
            <ItemTemplate>
                <asp:Label ID="lblFoodQuality" runat="server" Text='<%# Bind("FoodQualityRating") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="TextBoxFoodQuality" runat="server" Text='<%# Bind("FoodQualityRating") %>'></asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Service">
            <ItemTemplate>
                <asp:Label ID="lblService" runat="server" Text='<%# Bind("ServiceRating") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="TextBoxService" runat="server" Text='<%# Bind("ServiceRating") %>'></asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Atmosphere">
            <ItemTemplate>
                <asp:Label ID="lblAtmosphere" runat="server" Text='<%# Bind("AtmosphereRating") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="TextBoxAtmosphere" runat="server" Text='<%# Bind("AtmosphereRating") %>'></asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Price">
            <ItemTemplate>
                <asp:Label ID="lblPrice" runat="server" Text='<%# Bind("PriceLevelRating") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="TextBoxPrice" runat="server" Text='<%# Bind("PriceLevelRating") %>'></asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateField>

         <asp:TemplateField HeaderText="Comment">
            <ItemTemplate>
                <asp:Label ID="lblComment" runat="server" Text='<%# Bind("Comments") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="TextBoxComment" runat="server" Text='<%# Bind("Comments") %>'></asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateField>

        <asp:CommandField ButtonType="Button" ShowEditButton="True" />
    </Columns>
</asp:GridView>
        </div>
        <asp:Label ID="lblStatus" runat="server"></asp:Label>
        <asp:Button ID="btnBack" runat="server" Text="Back To Dashboard" OnClick="btnBack_Click"/>
            </ContentTemplate>
            </asp:UpdatePanel>
    </form>
</body>
</html>
