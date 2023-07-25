package SAR0083.Homework.models;

import java.time.LocalDate;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.ManyToOne;

import org.springframework.format.annotation.DateTimeFormat;
import org.springframework.format.annotation.DateTimeFormat.ISO;

@Entity
public class Lease {
	// Properties
	@Id
	@GeneratedValue(strategy = GenerationType.IDENTITY)
	private long id;
	@Column(nullable = false)
	@DateTimeFormat(iso = ISO.DATE)
	private LocalDate startDate;
	@Column(nullable = false)
	@DateTimeFormat(iso = ISO.DATE)
	private LocalDate endDate;
	@Column(nullable = false)
	private boolean insurance;
	
	@ManyToOne
	private Vehicle vehicle = new Vehicle();
	@ManyToOne
	private Customer customer = new Customer();
	
	
	// Constructors
	public Lease() {
		super();
	}
	
	public Lease(LocalDate startDate, LocalDate endDate, boolean insurance, Vehicle vehicle, Customer customer) throws Exception {
		super();
		setStartDate(startDate);
		setEndDate(endDate);
		setInsurance(insurance);
		setVehicle(vehicle);
		setCustomer(customer);
	}
	
	
	// Getters/setters: id
	public long getId() {
		return id;
	}
	
	public void setId(long id) {
		this.id = id;
	}
	
	
	// Getters/setters: startDate
	public LocalDate getStartDate() {
		return startDate;
	}
	
	public void setStartDate(LocalDate startDate) throws Exception {
		// Check date
		if(endDate != null && startDate.isAfter(endDate)) {
			throw new Exception("The start date must be before the end date!");
		}
		
		// Valid
		this.startDate = startDate;
	}
	
	
	// Getters/setters: endDate
	public LocalDate getEndDate() {
		return endDate;
	}
	
	public void setEndDate(LocalDate endDate) throws Exception {
		// Check date
		if(startDate != null && endDate.isBefore(startDate)) {
			throw new Exception("The end date must be after the start date!");
		}
		
		// Valid
		this.endDate = endDate;
	}
	
	
	// Getters/setters: insurance
	public boolean getInsurance() {
		return insurance;
	}
	
	public void setInsurance(boolean insurance) {
		this.insurance = insurance;
	}
	
	
	// Getters/setters: vehicle
	public Vehicle getVehicle() {
		return vehicle;
	}
	
	public void setVehicle(Vehicle vehicle) {
		this.vehicle = vehicle;
	}
	
	
	// Getters/setters: customer
	public Customer getCustomer() {
		return customer;
	}
	
	public void setCustomer(Customer customer) {
		this.customer = customer;
	}

	
	// Methods: override
	@Override
	public String toString() {
		return "#" + getId() + "; " + getStartDate() + "; " + getEndDate() + "; " + getInsurance();
	}
}