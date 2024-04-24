package com.example;

import java.io.FileOutputStream;
import java.io.IOException;
import java.nio.file.Paths;
import java.util.*;

import org.apache.poi.xssf.streaming.SXSSFCell;
import org.apache.poi.xssf.streaming.SXSSFRow;
import org.apache.poi.xssf.streaming.SXSSFSheet;
import org.apache.poi.xssf.streaming.SXSSFWorkbook;

public final class ExcelWriter {
    private final List<ExcelSheet> sheets;

    private ExcelWriter(String sheetName, List<List<Object>> rows) {
        this.sheets = new ArrayList<>();
        this.withSheet(sheetName, rows);
    }

    public ExcelWriter withSheet(String sheetName, List<List<Object>> rows) {
        this.sheets.add(new ExcelSheet(sheetName, rows));
        return this;
    }

    public void write() {
        String output = Paths.get(".", "example.xlsx").toAbsolutePath().toString();

        try (SXSSFWorkbook xssfwb = new SXSSFWorkbook(); FileOutputStream fos = new FileOutputStream(output)) {
            for (ExcelSheet excelSheet : this.sheets) {
                excelSheet.write(xssfwb);
            }
            xssfwb.write(fos);
            xssfwb.dispose();
        } catch (IOException e) {
            throw new RuntimeException(e);
        }
    }

    public static ExcelWriter on(String sheetName, List<List<Object>> rows) {
        return new ExcelWriter(sheetName, rows);
    }

    private static class ExcelSheet {
        private final String sheetName;
        private final List<List<Object>> rows;

        public ExcelSheet(String sheetName, List<List<Object>> rows) {
            this.sheetName = sheetName;
            this.rows = rows;
        }

        public void write(SXSSFWorkbook workbook) throws IOException {
            SXSSFSheet sheet = workbook.createSheet(this.sheetName);
            for (int j = 0; j < this.rows.size(); j++) {
                SXSSFRow sheetRow = sheet.createRow(j);
                List<Object> row = this.rows.get(j);
                for (int k = 0; k < row.size(); k++) {
                    SXSSFCell sheetCell = sheetRow.createCell(k);
                    Object value = row.get(k);
                    if (value instanceof String) {
                        sheetCell.setCellValue((String) value);
                    } else if (value instanceof Number) {
                        sheetCell.setCellValue(((Number) value).doubleValue());
                    } else if (value instanceof Date) {
                        sheetCell.setCellValue((Date) value);
                    } else {
                        sheetCell.setCellValue(value != null ? value.toString() : "");
                    }
                }
            }
            sheet.flushRows();
        }
    }
}
