package SAR0083.Homework.models;

import java.time.LocalDate;
import java.time.Period;
import java.util.List;

import javax.persistence.CascadeType;
import javax.persistence.Column;
import javax.persistence.Embedded;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.OneToMany;

import org.hibernate.annotations.OnDelete;
import org.hibernate.annotations.OnDeleteAction;
import org.springframework.format.annotation.DateTimeFormat;
import org.springframework.format.annotation.DateTimeFormat.ISO;

import com.fasterxml.jackson.annotation.JsonIgnore;

@Entity
public class Customer {
	// Properties
	@Id
	@GeneratedValue(strategy = GenerationType.IDENTITY)
	private long id;
	@JsonIgnore
	@Embedded
	private FullName fullName = new FullName();
	@Column(nullable = false)
	@DateTimeFormat(iso = ISO.DATE)
	private LocalDate birthDate;
	@Embedded
	private EmailAddress emailAddress = new EmailAddress();
	@Embedded
	private PhoneNumber phoneNumber = new PhoneNumber();
	
	@JsonIgnore
	@OneToMany(mappedBy = "customer", cascade = CascadeType.REMOVE)
	@OnDelete(action = OnDeleteAction.CASCADE)
	private List<Lease> leases;
	
	
	// Constructors
	public Customer() {
		super();
	}
	
	public Customer(FullName fullName, LocalDate birthDate, EmailAddress emailAddress, PhoneNumber phoneNumber) throws Exception {
		super();
		setFullName(fullName);
		setBirthDate(birthDate);
		setEmailAddress(emailAddress);
		setPhoneNumber(phoneNumber);
	}
	
	
	// Getters/setters: id
	public long getId() {
		return id;
	}
	
	public void setId(long id) {
		this.id = id;
	}
	
	
	// Getters/setters: fullName
	public FullName getFullName() {
		return fullName;
	}
	
	public void setFullName(FullName fullName) {
		this.fullName = fullName;
	}
	
	public void setFullName(String fullName) throws Exception {
		this.fullName = new FullName(fullName);
	}
	
	// Getters/setters: fullName / firstName
	public String getFirstName() {
		return fullName.getFirstName();
	}
	
	public void setFirstName(String firstName) throws Exception {
		this.fullName.setFirstName(firstName);
	}
	
	// Getters/setters: fullName / lastName
	public String getLastName() {
		return fullName.getLastName();
	}
	
	public void setLastName(String lastName) throws Exception {
		this.fullName.setLastName(lastName);
	}
	
	
	// Getters/setters: birthDate
	public LocalDate getBirthDate() {
		return birthDate;
	}
	
	public void setBirthDate(LocalDate birthDate) throws Exception {
		// Check age
		if(Period.between(birthDate, LocalDate.now()).getYears() < 18) {
			throw new Exception("The customer must be a minimum of 18 years old!");
		}
		
		// Valid
		this.birthDate = birthDate;
	}
	
	// Getters/setters: birthDate / age
	@JsonIgnore
	public int getAge() {
		return Period.between(birthDate, LocalDate.now()).getYears();
	}
	
	
	// Getters/setters: emailAddress
	public EmailAddress getEmailAddress() {
		return emailAddress;
	}
	
	public void setEmailAddress(EmailAddress emailAddress) {
		this.emailAddress = emailAddress;
	}
	
	
	// Getters/setters: phoneNumber
	public PhoneNumber getPhoneNumber() {
		return phoneNumber;
	}
	
	public void setPhoneNumber(PhoneNumber phoneNumber) {
		this.phoneNumber = phoneNumber;
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
		return "#" + getId() + "; " + getFullName() + "; " + getAge() + " years old; " + getEmailAddress() + "; " + getPhoneNumber();
	}
}