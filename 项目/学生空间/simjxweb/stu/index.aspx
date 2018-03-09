<%@ Page Title="" Language="C#" MasterPageFile="~/stu/LX.master" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="stu_index" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%--    <style type="text/css">
 .sort-item{
   color: #f01400;
   border-bottom: 2px solid #f01400;
   border-bottom-width: 2px;
   border-bottom-style: solid;
   border-bottom-color: rgb(240, 20, 0);
            }
    </style>--%>

    <div style=" font-size: 14px;
    height: 58px;
    line-height: 58px;
    margin-top: 10px;
    border-bottom: 1px solid #d0d6d9;
    border-bottom-width: 1px;
    border-bottom-style: solid;
    border-bottom-color: rgb(208, 214, 217);">
      
       <div style="float: left;">
          
        <a href="#" style="color: red;border-bottom: 2px solid #f01400;
                border-bottom-width: 2px;
                border-bottom-style: solid;
                border-bottom-color: rgb(240, 20, 0)">最近学习</a>
        </div> 
        <div style="float: left;margin-left:20px;">
         <a href="#" style="color: red;border-bottom: 2px solid #f01400;
                border-bottom-width: 2px;
                border-bottom-style: solid;
                border-bottom-color: rgb(240, 20, 0)">已完课程</a>
             </div>
        <div style="float: right;">
            <div style="position: relative;margin-right: 10px;">
                <span class="cursor: pointer">全部课程</span>
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
         <%--<div style="content: '';
                     position: absolute;
                     width: 1px;
                     height: 20px;
                     left: -1px;
                     top: 0;
                     background-color: #fff;"></div>--%>
            <span style="position: absolute;left: 0;top: 20px;margin-left: -64px;line-height: 16px;font-size: 12px;color: #8a8c8f;">
               
                <b style="font-size: 18px;font-weight: 400;">
                    <asp:Label ID="YEAR" runat="server" ></asp:Label></b>
                <em style="display: block;margin-top: 2px;">
                    <asp:Label ID="DAY" runat="server" Text=""></asp:Label></em>
            </span>
            <div style="padding-top:40px;">
                <asp:DataList ID="DataList1" runat="server" RepeatColumns="1"><%--绑定数据--%>
                <ItemTemplate>
            <div style="padding: 30px 0;padding-top: 30px;padding-right: 0px;padding-bottom: 30px;padding-left: 0px;position: relative;border-bottom: 1px solid #eff1f0;border-bottom-width: 1px; border-bottom-style: solid;border-bottom-color: rgb(239, 241, 240);">
                <asp:Image ID="Image1" width="150" height="70" runat="server" ImageUrl='<%#Eval("imagepath")%>'/>
                 <div style ="float:right;padding-left:60px;">
                <h3 style="font-size: 18px;color:black;height: 29px;line-height: 29px;position: relative;">
                 <a href='<%#Eval("programpath") %>' target=""> <asp:Label ID="Label1" runat="server" Text='<%#Eval("keyword") %>'></asp:Label></a>
                    <span style="    font-size: 14px;color: #787d82;">更新完毕</span>
                </h3>
                <div style="padding: 5px 0 10px;padding-top: 5px;padding-right: 0px;padding-bottom: 10px;padding-left: 0px;height: 21px;">
                    <span style="font-size: 14px;color: #f01400;">
                        用时<asp:Label ID="time" runat="server" Text=""></asp:Label></span><%#Eval("time") %>
                    <span style="font-size: 14px;color: #787d82;">学习至2-4 流水行船-提问（prompt...</span>
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

