package SAR0083.Homework.models;

import javax.persistence.Column;
import javax.persistence.DiscriminatorValue;
import javax.persistence.Entity;

@Entity
@DiscriminatorValue("2")
public class Truck extends Vehicle {
	// Properties
	@Column(length = 1)
	private short trailerCount = 0;
	
	
	// Constructors
	public Truck() {
		super();
	}
	
	public Truck(String brand, String model, short modelYear, float weight) throws Exception {
		super(brand, model, modelYear, weight);
	}
	
	public Truck(String brand, String model, short modelYear, float weight, short trailerCount) throws Exception {
		this(brand, model, modelYear, weight);
		setTrailerCount(trailerCount);
	}
	
	
	// Getters/setters: trailerCount
	public short getTrailerCount() {
		return trailerCount;
	}
	
	public void setTrailerCount(short trailerCount) throws Exception {
		// Check count
		if(trailerCount < 0) {
			throw new Exception("The trailer count is too low, it must be a minimum of 0!");
		}
		else if(trailerCount > 4) {
			throw new Exception("The trailer count is too high, it must be a maximum of 4!");
		}
		
		// Valid
		this.trailerCount = trailerCount;
	}
	
	
	// Methods: override
	@Override
	public String toString() {
		return "#" + getId() + "; " + getBrand() + "; " + getModel() + "; " + getModelYear() + "; " + getWeight() + " kg; Trailer Count: " + getTrailerCount();
	}
}