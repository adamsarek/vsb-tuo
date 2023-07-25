package SAR0083.Homework.models;

import java.util.List;

import javax.persistence.CascadeType;
import javax.persistence.Column;
import javax.persistence.DiscriminatorColumn;
import javax.persistence.DiscriminatorType;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.Inheritance;
import javax.persistence.InheritanceType;
import javax.persistence.OneToMany;

import org.hibernate.annotations.OnDelete;
import org.hibernate.annotations.OnDeleteAction;

import com.fasterxml.jackson.annotation.JsonIgnore;

@Entity
@Inheritance(strategy = InheritanceType.SINGLE_TABLE)
@DiscriminatorColumn(name = "vehicle_type", discriminatorType = DiscriminatorType.INTEGER)
public class Vehicle {
	// Properties
	@Id
	@GeneratedValue(strategy = GenerationType.IDENTITY)
	private long id;
	@Column(length = 32, nullable = false)
	private String brand;
	@Column(length = 32, nullable = false)
	private String model;
	@Column(nullable = false)
	private short modelYear;
	@Column(nullable = false)
	private float weight;
	@Column(name = "vehicle_type", nullable = false, insertable = false, updatable = false)
	private int vehicleType;
	
	@JsonIgnore
	@OneToMany(mappedBy = "vehicle", cascade = CascadeType.REMOVE)
	@OnDelete(action = OnDeleteAction.CASCADE)
	private List<Lease> leases;
	
	
	// Constructors
	public Vehicle() {
		super();
	}
	
	public Vehicle(String brand, String model, short modelYear, float weight) throws Exception {
		super();
		setBrand(brand);
		setModel(model);
		setModelYear(modelYear);
		setWeight(weight);
	}
	
	
	// Getters/setters: id
	public long getId() {
		return id;
	}
	
	public void setId(long id) {
		this.id = id;
	}
	
	
	// Getters/setters: brand
	public String getBrand() {
		return brand;
	}
	
	public void setBrand(String brand) throws Exception {
		// Check length
		if(brand.length() < 1) {
			throw new Exception("The brand is too short, it must have a minimum of 1 character!");
		}
		else if(brand.length() > 32) {
			throw new Exception("The brand is too long, it must have a maximum of 32 characters!");
		}
		
		// Valid
		this.brand = brand;
	}
	
	
	// Getters/setters: model
	public String getModel() {
		return model;
	}
	
	public void setModel(String model) throws Exception {
		// Check length
		if(model.length() < 1) {
			throw new Exception("The model is too short, it must have a minimum of 1 character!");
		}
		else if(model.length() > 32) {
			throw new Exception("The model is too long, it must have a maximum of 32 characters!");
		}
		
		// Valid
		this.model = model;
	}
	
	
	// Getters/setters: modelYear
	public short getModelYear() {
		return modelYear;
	}
	
	public void setModelYear(short modelYear) throws Exception {
		// Check valid year
		if(modelYear < 1886) {
			throw new Exception("The model year must be a minimum of 1886!");
		}
		
		// Valid
		this.modelYear = modelYear;
	}
	
	
	// Getters/setters: weight
	public float getWeight() {
		return weight;
	}
	
	public void setWeight(float weight) throws Exception {
		// Check positive value
		if(weight < 0) {
			throw new Exception("The weight must be a minimum of 0 kg!");
		}
		
		// Valid
		this.weight = weight;
	}
	
	
	// Getters/setters: vehicleType
	public int getVehicleType() {
		return vehicleType;
	}
	
	public void setVehicleType(int vehicleType) {
		this.vehicleType = vehicleType;
	}
	
	
	// Getters/setters: leases
	public List<Lease> getLeases() {
		return leases;
	}
	
	public void setLeases(List<Lease> leases) {
		this.leases = leases;
	}

	
	// Methods: override
	@Override
	public String toString() {
		return "#" + getId() + "; " + getBrand() + "; " + getModel() + "; " + getModelYear() + "; " + getWeight() + " kg";
	}
}