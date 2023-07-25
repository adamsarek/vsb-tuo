package SAR0083.Homework.models;

import javax.persistence.Column;
import javax.persistence.DiscriminatorValue;
import javax.persistence.Entity;
import javax.persistence.EnumType;
import javax.persistence.Enumerated;
import javax.persistence.Transient;

import com.fasterxml.jackson.annotation.JsonIgnore;

@Entity
@DiscriminatorValue("1")
public class Car extends Vehicle {
	// Properties
	@JsonIgnore
	@Column(columnDefinition = "INTEGER", length = 1)
	@Enumerated(EnumType.ORDINAL)
	private Trunk trunk = Trunk.None;
	@Transient
	private int trunkValue = 0;
	
	
	// Constructors
	public Car() {
		super();
	}
	
	public Car(String brand, String model, short modelYear, float weight) throws Exception {
		super(brand, model, modelYear, weight);
	}
	
	public Car(String brand, String model, short modelYear, float weight, Trunk trunk) throws Exception {
		this(brand, model, modelYear, weight);
		setTrunk(trunk);
	}
	
	
	// Getters/setters: trunk
	public Trunk getTrunk() {
		return trunk;
	}
	
	public void setTrunk(Trunk trunk) {
		this.trunk = trunk;
	}
	
	
	// Getters/setters: trunkValue
	public int getTrunkValue() {
		return trunk.getValue();
	}
	
	public void setTrunkValue(int trunk) {
		this.trunk = Trunk.fromInteger(trunk);
	}
	
	
	// Methods: override
	@Override
	public String toString() {
		return "#" + getId() + "; " + getBrand() + "; " + getModel() + "; " + getModelYear() + "; " + getWeight() + " kg; Trunk: " + getTrunk();
	}
}