<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Solding.aspx.cs" Inherits="Solding" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" AllowPaging="True">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />
                <asp:BoundField DataField="Image" HeaderText="Image" SortExpression="Image" />
                <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                <asp:BoundField DataField="ItemsLeft" HeaderText="ItemsLeft" SortExpression="ItemsLeft" />
                <asp:BoundField DataField="Type" HeaderText="Type" SortExpression="Type" />
                <asp:BoundField DataField="PriceForOne" HeaderText="PriceForOne" SortExpression="PriceForOne" />
                <asp:BoundField DataField="ActualPrice" HeaderText="ActualPrice" SortExpression="ActualPrice" />
                <asp:BoundField DataField="EndDate" HeaderText="EndDate" SortExpression="EndDate" />
            </Columns>
        </asp:GridView>
    
    </div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RegisterInput %>" SelectCommand="SELECT I.Id, I.Image , I.Title, IP.ItemsLeft , IP.Type , IP.PriceForOne , IP.ActualPrice , IP.EndDate  FROM 
                InProgress as IP 
                LEFT JOIN Items as I ON I.id = IP.IdItem
		LEFT JOIN Users as U ON U.ID = I.IdSeller
                WHERE U.ID like ' + UserID + '"></asp:SqlDataSource>
    </form>
</body>
</html>
