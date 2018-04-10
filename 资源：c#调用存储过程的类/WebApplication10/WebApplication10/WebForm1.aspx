<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication10.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
        <asp:Button ID="Button1" runat="server" Height="51px" OnClick="Button1_Click" Text="删除" Width="78px" />
        <p>
            <asp:Button ID="Button3" runat="server" Height="31px" OnClick="Button3_Click" Text="修改" Width="70px" />
        </p>
        <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="插入" />
    </form>
</body>
</html>
