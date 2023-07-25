package tools.enumerations;

public enum PlayStatus{
    STARTED(0),
    PLAYING(1),
    PAUSED (2),
    ENDED  (3);

    private final int status;

    private PlayStatus(int status){
        this.status = status;
    }
}
