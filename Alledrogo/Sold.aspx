<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Sold.aspx.cs" Inherits="Sold" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />
                <asp:BoundField DataField="Image" HeaderText="Image" SortExpression="Image" />
                <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                <asp:BoundField DataField="Type" HeaderText="Type" SortExpression="Type" />
                <asp:BoundField DataField="NumberOfItems" HeaderText="NumberOfItems" SortExpression="NumberOfItems" />
                <asp:BoundField DataField="Price" HeaderText="Price" SortExpression="Price" />
                <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
            </Columns>
        </asp:GridView>
    
    </div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RegisterInput %>" SelectCommand="SELECT I.Id, I.Image , I.Title ,B.Type , B.NumberOfItems , B.Price , B.Date FROM 
                Bought as B 
                LEFT JOIN Items as I ON I.id = B.Iditem
		LEFT JOIN Users as U ON U.ID = I.IdSeller
                WHERE U.ID like ' + UserID + '"></asp:SqlDataSource>
    </form>
</body>
</html>
