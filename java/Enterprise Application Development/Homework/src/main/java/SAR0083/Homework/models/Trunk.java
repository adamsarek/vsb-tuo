package SAR0083.Homework.models;

public enum Trunk {
	// Enumerations
	None(0),
	Rear(1),
	Front(2),
	Both(3);
	
	
	// Properties
	private final int value;
	
	
	// Constructors
	Trunk(final int value) {
		this.value = value;
	}
	
	
	// Methods
	public static Trunk fromInteger(int value) {
		switch(value) {
			case 1:
				return Rear;
			case 2:
				return Front;
			case 3:
				return Both;
			default:
				return None;
		}
	}
	
	public int getValue() {
		return value;
	}
}