import React, { useState } from "react";
import { Layout, Menu, Breadcrumb } from "antd";
import {
  HomeOutlined,
  OrderedListOutlined,
  LineChartOutlined,
} from "@ant-design/icons";
import { Content, Footer, Header } from "antd/es/layout/layout";
import Transacao from "../transacao/Index";
import PlanoConta from "../planoConta/Index";

const Home: React.FC = () => {
  const [selectedKey, setSelectedKey] = useState("1");

  const handleMenuClick = (e: any) => {
    setSelectedKey(e.key);
  };

  const renderContent = () => {
    switch (selectedKey) {
      case "1":
        return <Transacao />;
      case "2":
        return <PlanoConta />;
      case "3":
        return <div>Dashboard Content</div>;
      default:
        return null;
    }
  };

  return (
    <Layout className="layout">
      <Header>
        <div className="logo" />
        <Menu
          theme="dark"
          mode="horizontal"
          defaultSelectedKeys={["1"]}
          onClick={handleMenuClick}
        >
          <Menu.Item key="1" icon={<HomeOutlined />}>
            Transações
          </Menu.Item>
          <Menu.Item key="2" icon={<OrderedListOutlined />}>
            Tipo Transações
          </Menu.Item>
          <Menu.Item key="3" icon={<LineChartOutlined />}>
            Dashboard
          </Menu.Item>
        </Menu>
      </Header>
      <Content style={{ padding: "0 50px", flex: 1, minHeight: "100vh" }}>
        <Breadcrumb style={{ margin: "16px 0" }}>
          <Breadcrumb.Item>Home</Breadcrumb.Item>
          <Breadcrumb.Item>
            {selectedKey === "1"
              ? "Transações"
              : selectedKey === "2"
              ? "Tipo Transações"
              : "Dashboard"}
          </Breadcrumb.Item>
        </Breadcrumb>
        <div className="site-layout-content">{renderContent()}</div>
      </Content>
      <Footer style={{ textAlign: "center" }}>
        Ant Design ©2024 Created by Ant UED
      </Footer>
    </Layout>
  );
};
export default Home;
