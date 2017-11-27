<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Solding.aspx.cs" Inherits="Solding" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            text-align: right;
        }
        .auto-style3 {
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
        <table class="auto-style1">
            <tr>
                <td class="auto-style3">
                    <asp:Label ID="Label1" runat="server" Text="Kup teraz"></asp:Label>
                </td>
                <td class="auto-style3">
                    <asp:Label ID="Label2" runat="server" Text="Licytacja"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
    
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" AllowPaging="True">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />
                <asp:BoundField DataField="Image" HeaderText="Image" SortExpression="Image" />
                <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                <asp:BoundField DataField="ItemsLeft" HeaderText="ItemsLeft" SortExpression="ItemsLeft" />
                <asp:BoundField DataField="Type" HeaderText="Type" SortExpression="Type" />
                <asp:BoundField DataField="PriceForOne" HeaderText="PriceForOne" SortExpression="PriceForOne" />
                <asp:BoundField DataField="EndDate" HeaderText="EndDate" SortExpression="EndDate" />
            </Columns>
        </asp:GridView>
    
                </td>
                <td class="auto-style2">
                    <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataSourceID="SqlDataSource2">
                        <Columns>
                            <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />
                            <asp:BoundField DataField="Image" HeaderText="Image" SortExpression="Image" />
                            <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                            <asp:BoundField DataField="Type" HeaderText="Type" SortExpression="Type" />
                            <asp:BoundField DataField="CenaWykupu" HeaderText="CenaWykupu" SortExpression="CenaWykupu" />
                            <asp:BoundField DataField="ActualPrice" HeaderText="ActualPrice" SortExpression="ActualPrice" />
                            <asp:BoundField DataField="EndDate" HeaderText="EndDate" SortExpression="EndDate" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RegisterInput %>" SelectCommand="SELECT I.Id, I.Image , I.Title, IP.ItemsLeft , IP.Type , IP.PriceForOne ,  IP.EndDate  FROM 
                InProgress as IP 
                LEFT JOIN Items as I ON I.id = IP.IdItem
		LEFT JOIN Users as U ON U.ID = I.IdSeller
                WHERE U.ID like ' + UserID + '" OnSelecting="SqlDataSource1_Selecting"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:RegisterInput %>" SelectCommand="SELECT I.Id, I.Image , I.Title , IP.Type , IP.PriceForOne as CenaWykupu , IP.ActualPrice , IP.EndDate  FROM
                InProgress as IP 
                LEFT JOIN Items as I ON I.id = IP.IdItem
		LEFT JOIN Users as U ON U.ID = I.IdSeller
                WHERE U.ID like ' + UserID + '"></asp:SqlDataSource>
    </form>
</body>
</html>
