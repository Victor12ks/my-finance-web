import React, { useEffect } from "react";
import type { TimeRangePickerProps } from "antd";
import { DatePicker, Space } from "antd";
import dayjs from "dayjs";
import type { Dayjs } from "dayjs";

const { RangePicker } = DatePicker;

const rangePresets: TimeRangePickerProps["presets"] = [
  { label: "Últimos 7 Dias", value: [dayjs().add(-7, "d"), dayjs()] },
  { label: "Últimos 14 Dias", value: [dayjs().add(-14, "d"), dayjs()] },
  { label: "Últimos 30 Dias", value: [dayjs().add(-30, "d"), dayjs()] },
  { label: "Últimos 90 Dias", value: [dayjs().add(-90, "d"), dayjs()] },
];

interface SelectDateProps {
  onSelect: (datas: { dataInicial: string; dataFinal: string }) => void;
}

const SelectDate: React.FC<SelectDateProps> = ({ onSelect }) => {
  const onRangeChange = (
    dates: null | (Dayjs | null)[],
    dateStrings: string[]
  ) => {
    if (dates) {
      onSelect({
        dataInicial: dates[0]?.format("MM-DD-YYYY") ?? "",
        dataFinal: dates[1]?.format("MM-DD-YYYY") ?? "",
      });
    }
  };

  useEffect(() => {
    onSelect({
      dataFinal: dayjs()?.format("MM-DD-YYYY") ?? "",
      dataInicial: dayjs().add(-30, "d").format("MM-DD-YYYY") ?? "",
    });
  }, []);

  return (
    <Space direction="vertical" size={12}>
      <RangePicker
        presets={rangePresets}
        onChange={onRangeChange}
        format="DD/MM/YYYY"
        defaultValue={[dayjs().add(-30, "d"), dayjs()]}
        placeholder={["Data Inicial", "Data Final"]}
      />
    </Space>
  );
};

export default SelectDate;
