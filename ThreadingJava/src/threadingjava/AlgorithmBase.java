package threadingjava;

import java.awt.image.BufferedImage;
import javafx.embed.swing.SwingFXUtils;
import javafx.scene.image.WritableImage;

public abstract class AlgorithmBase {

    protected int width = 600;
    protected int height = 600;

    protected long startTime;
    protected long stopTime;

    protected final BufferedImage image;

    public AlgorithmBase() {
        image = new BufferedImage(width, height, BufferedImage.TYPE_INT_ARGB);
    }

    public int getWidth() {
        return width;
    }

    public int getHeight() {
        return height;
    }

    public void setWidth(int width) {
        this.width = width;
    }

    public void setHeight(int height) {
        this.height = height;
    }

    public BufferedImage getImage() {
        return image;
    }

    public WritableImage getImageFX() {
        return SwingFXUtils.toFXImage(getImage(), null);
    }

    public void startTimeMeasurement() {
        startTime = System.currentTimeMillis();
    }

    public void stopTimeMeasurement() {
        stopTime = System.currentTimeMillis();
    }

    public double getDuration() {
        double tmp = (stopTime - startTime) / 1000.0;
        return Math.round(tmp * 1000) / 1000.0;
    }

    public void solve() {
        for (int i = 0; i < height; i++) {
            for (int j = 0; j < width; j++) {
                solvePoint(j, i);
            }
        }
    }

    public void solveRow(int row) {
        int i = row;
        for (int j = 0; j < width; j++) {
            solvePoint(j, i);
        }
    }

    public void solvePart(int part, int totalNumOfParts) {
        int w = getHeight() / totalNumOfParts;
        int row = part * w;

        for (int i = 0; i < w; i++) {
            for (int j = 0; j < getWidth(); j++) {
                solvePoint(j, row);
            }
            row++;
        }
    }

    public abstract void solvePoint(int j, int i);

}
