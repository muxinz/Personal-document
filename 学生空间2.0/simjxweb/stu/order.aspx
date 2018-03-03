<%@ Page Title="" Language="C#" MasterPageFile="~/stu/LX.master" AutoEventWireup="true" CodeFile="order.aspx.cs" Inherits="stu_order" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div style=" font-size: 14px;
    height: 58px;
    line-height: 58px;
    margin-top: 10px;
    border-bottom: 1px solid #d0d6d9;
    border-bottom-width: 1px;
    border-bottom-style: solid;
    border-bottom-color: rgb(208, 214, 217);">
      
       <div style="float: left;">
          
        <a href="order.aspx" style="color: red;border-bottom: 2px solid #f01400;
                border-bottom-width: 2px;
                border-bottom-style: solid;
                border-bottom-color: rgb(240, 20, 0)">购买记录</a>
        </div> 
        <div style="float: left;margin-left:20px;">
         <a href="order_a.aspx" style="color: red;border-bottom: 2px solid #f01400;
                border-bottom-width: 2px;
                border-bottom-style: solid;
                border-bottom-color: rgb(240, 20, 0)">我的优惠</a>
             </div>
       <%-- <div style="float: left;margin-left:20px;">
         <a href="#" style="color: red;border-bottom: 2px solid #f01400;
                border-bottom-width: 2px;
                border-bottom-style: solid;
                border-bottom-color: rgb(240, 20, 0);">课程咨询</a>
             </div>--%>

        <div style="float: right;">
            <div style="position: relative;margin-right: 10px;">
                <span class="cursor: pointer">全部记录</span>
            </div>

        </div>
    </div>
    <div style=" padding: 0 0 0 64px;
                 padding-top: 0px;
                 padding-right: 0px;
                 padding-bottom: 0px;
                 padding-left: 64px;">
        <div style="position: relative;
                    padding-left: 37px;
                    border-left: 1px solid #d3d7da;
                    border-left-width: 1px;
                    border-left-style: solid;
                    border-left-color: rgb(211, 215, 218);">
         <div style="content: '';
                     position: absolute;
                     width: 1px;
                     height: 20px;
                     left: -1px;
                     top: 0;
                     background-color: #fff;"></div>
            <span style="position: absolute;left: 0;top: 20px;margin-left: -64px;line-height: 16px;font-size: 12px;color: #8a8c8f;">
               
                <b style="font-size: 18px;font-weight: 400;">数量</b>
                <em style="display: block;margin-top: 2px;text-decoration:none;"> <asp:Label ID="countid" runat="server" Text=""></asp:Label></em>
            </span>
            
             <div style="padding-top:40px;">
           <asp:DataList ID="DataList1" runat="server" RepeatColumns="1">
                <ItemTemplate>
            <div style="padding: 30px 0;padding-top: 30px;padding-right: 0px;padding-bottom: 30px;padding-left: 0px;position: relative;border-bottom: 1px solid #eff1f0;border-bottom-width: 1px; border-bottom-style: solid;border-bottom-color: rgb(239, 241, 240);">
                <asp:Image ID="Image1" runat="server" width="150" height="70" ImageUrl='<%#Eval("imagepath") %>' />
            <%--<div style="padding-left: 230px; position:relative;">--%>
                    <%--<div style="padding-left: 230px; float:right;">--%>
                <%--<div style ="position:relative;top:-100px;left:20px">--%>
               <div style ="float:right;padding-left:60px;padding-top:-20px;">
                <h3 style="font-size: 18px;color: #12171b;height: 29px;line-height: 29px;position: relative;">
                    <%--题目类型 关键字--%>
                    <a href='<%#Eval("programpath") %>' target=""> <asp:Label ID="Label1" runat="server" Text='<%#Eval("keyword")%>'> </asp:Label></a>
                    <span style="    font-size: 14px;color: #787d82;">课程ID：<asp:Label ID="courseid" runat="server" Text='<%#Eval("courseid")%>'>
                     </asp:Label></span>
                </h3>
                <div style="padding: 5px 0 10px;padding-top: 5px;padding-right: 0px;padding-bottom: 10px;padding-left: 0px;height: 21px;">
                    <span style="color: #f01400;">购买时间：</span>
                    <span style="font-size: 14px;color: #787d82;"><asp:Label ID="buytime" runat="server" Text='<%#Eval("paytime") %>'></asp:Label></span>
                    <span style="font-size: 14px;color: #787d82;">已学习至48%</span><br/>
                    <span style="color: #f01400;">购买金额：</span>
                    <span style="font-size: 14px;color: #787d82;"><asp:Label ID="buymoney" runat="server" Text='<%#Eval("proprice") %>'></asp:Label>￥
                    </span>
                </div>           
                </div>
            </div>
                </ItemTemplate>
                </asp:DataList>
                 <div style="padding-top:20px">
                 <asp:Label ID="pagecount" runat="server" Text=""></asp:Label>
                 <asp:Button ID="first" runat="server" Text="首页" OnClick="Button2_Click" />
                <asp:Button ID="page_up" runat="server" Text="上一页" OnClick="Button3_Click" />
                <asp:Label ID="current" runat="server" Text="1"></asp:Label>
                <asp:Button ID="page_down" runat="server" Text="下一页" OnClick="Button1_Click" />
                 </div>
            </div>
                
                </div>
    </div>

</asp:Content>

