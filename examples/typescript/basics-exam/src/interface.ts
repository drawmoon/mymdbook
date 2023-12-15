interface TableField {
  id: number;
  type: string;
  filter: string;
}

const tableField: TableField = {
  id: 1,
  type: 'Measure',
  filter: 'price is not null',
};