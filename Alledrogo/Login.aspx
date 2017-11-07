<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 342px;
        }
        .auto-style3 {
            text-align: right;
        }
        .auto-style4 {
            width: 487px;
        }
        .auto-style5 {
            width: 842px;
        }
        .auto-style6 {
            width: 487px;
            text-align: left;
        }
        .auto-style7 {
            width: 842px;
            text-align: right;
        }
        .auto-style8 {
            width: 842px;
            height: 102px;
        }
        .auto-style9 {
            width: 487px;
            height: 102px;
            font-weight: 700;
            font-size: xx-large;
        }
        .auto-style10 {
            width: 342px;
            height: 102px;
        }
        .auto-style11 {
            height: 102px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table class="auto-style1">
            <tr>
                <td class="auto-style5">&nbsp;</td>
                <td class="auto-style4">&nbsp;</td>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style3">
                    <asp:Button ID="RegisterButton" runat="server" OnClick="RegisterButton_Click" Text="Rejestracja" />
                </td>
            </tr>
            <tr>
                <td class="auto-style5">&nbsp;</td>
                <td class="auto-style4">&nbsp;</td>
                <td class="auto-style2">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style8"></td>
                <td class="auto-style9">ALLEDROGO</td>
                <td class="auto-style10"></td>
                <td class="auto-style11"></td>
            </tr>
            <tr>
                <td class="auto-style7">E-mail:</td>
                <td class="auto-style6">
                    <asp:TextBox ID="Email" runat="server" Width="154px"></asp:TextBox>
                </td>
                <td class="auto-style2">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style7">Hasło:</td>
                <td class="auto-style6">
                    <asp:TextBox ID="Password" runat="server" Width="154px"></asp:TextBox>
                </td>
                <td class="auto-style2">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style5">&nbsp;</td>
                <td class="auto-style4">&nbsp;</td>
                <td class="auto-style2">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style5">&nbsp;</td>
                <td class="auto-style4">
                    <asp:Button ID="LoginButton" runat="server" Text="Zaloguj" OnClick="LoginButton_Click" />
                </td>
                <td class="auto-style2">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style5">&nbsp;</td>
                <td class="auto-style4">&nbsp;</td>
                <td class="auto-style2">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style5">&nbsp;</td>
                <td class="auto-style4">&nbsp;</td>
                <td class="auto-style2">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style5">&nbsp;</td>
                <td class="auto-style4">&nbsp;</td>
                <td class="auto-style2">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
