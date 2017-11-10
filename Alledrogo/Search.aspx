<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="Search" %>

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
            width: 351px;
        }
        .auto-style3 {
            width: 351px;
        }
        .auto-style4 {
            width: 197px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table class="auto-style1">
            <tr>
                <td class="auto-style4">
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RegisterInput %>" SelectCommand="select I.ID, I.Title as Tytuł ,IP.Type as 'Typ Aukcji' , IP.ItemsLeft as 'Ilość przedmiotów' , IP.PriceForOne as 'Cena za sztuke' , IP.ActualPrice as 'Cena Aktualna', U.Name +' '+U.surname as Sprzedawca , IP.EndDate as 'Data zakończenia aukcji'
from InProgress as IP
LEFT JOIN Items AS I ON IP.IdItem = I.Id
LEFT JOIN Users AS U ON I.IDSeller = U.ID
WHERE I.Title like concat('%','&quot;+SearchText.Text+&quot;','%')"></asp:SqlDataSource>
                </td>
                <td class="auto-style2">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4">&nbsp;</td>
                <td class="auto-style3">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4">&nbsp;</td>
                <td class="auto-style3">&nbsp;</td>
                <td>
                    <asp:Button ID="SearchButton" runat="server" Text="Szukaj" OnClick="SearchButton_Click" />
                &nbsp;
                    <asp:TextBox ID="SearchText" runat="server" Width="251px"></asp:TextBox>
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" Height="329px" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" Visible="False" Width="600px">
                        <Columns>
                            <asp:CommandField ShowSelectButton="True" />
                            <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
                            <asp:BoundField DataField="Tytuł" HeaderText="Tytuł" SortExpression="Tytuł" />
                            <asp:BoundField DataField="Typ Aukcji" HeaderText="Typ Aukcji" SortExpression="Typ Aukcji" />
                            <asp:BoundField DataField="Ilość przedmiotów" HeaderText="Ilość przedmiotów" SortExpression="Ilość przedmiotów" />
                            <asp:BoundField DataField="Cena za sztuke" HeaderText="Cena za sztuke" SortExpression="Cena za sztuke" />
                            <asp:BoundField DataField="Cena Aktualna" HeaderText="Cena Aktualna" SortExpression="Cena Aktualna" />
                            <asp:BoundField DataField="Sprzedawca" HeaderText="Sprzedawca" SortExpression="Sprzedawca" ReadOnly="True" />
                            <asp:BoundField DataField="Data zakończenia aukcji" HeaderText="Data zakończenia aukcji" SortExpression="Data zakończenia aukcji" />
                        </Columns>
                    </asp:GridView>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4">&nbsp;</td>
                <td class="auto-style3">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4">&nbsp;</td>
                <td class="auto-style3">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    <div>
    
    </div>
    </form>
</body>
</html>
