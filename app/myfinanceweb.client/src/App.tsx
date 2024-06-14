import React from "react";
import { Button, Space, DatePicker, version } from "antd";
import Home from "./pages/home/Index";

interface Forecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

const App = () => (
  <div className="App">
    <Home />
  </div>
);

export default App;

// const App = () => (
//   <div style={{ padding: "0 24px" }}>
//     <h1>antd version: {version}</h1>
//     <Space>
//       <DatePicker />
//       <Home />
//     </Space>
//   </div>
// );
