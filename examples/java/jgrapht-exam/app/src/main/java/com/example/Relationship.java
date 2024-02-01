package com.example;

public class Relationship {
    private int id;

    private int leftTableId;

    private int rightTableId;

    private String condition;

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public int getLeftTableId() {
        return leftTableId;
    }

    public void setLeftTableId(int leftTableId) {
        this.leftTableId = leftTableId;
    }

    public int getRightTableId() {
        return rightTableId;
    }

    public void setRightTableId(int rightTableId) {
        this.rightTableId = rightTableId;
    }

    public String getCondition() {
        return condition;
    }

    public void setCondition(String condition) {
        this.condition = condition;
    }
}
