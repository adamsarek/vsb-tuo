package SAR0083.Homework.models;

import java.util.List;
import java.util.stream.Collectors;

import javax.persistence.Column;
import javax.persistence.Embeddable;

@Embeddable
public class FullName {
	// Properties
	@Column(length = 32, nullable = false)
	private String firstName;
	@Column(length = 32, nullable = false)
	private String lastName;
	
	
	// Constructors
	public FullName() {
		super();
	}
	
	public FullName(String firstName, String lastName) throws Exception {
		super();
		setFirstName(firstName);
		setLastName(lastName);
	}
	
	public FullName(String fullName) throws Exception {
		super();
		String[] fullNameParts = fullName.split(" ");
		setFirstName(fullNameParts[0]);
		setLastName(fullNameParts[1]);
	}
	
	
	// Getters/setters: firstName
	public String getFirstName() {
		return firstName;
	}
	
	public void setFirstName(String firstName) throws Exception {
		// Check length
		if(firstName.length() < 1) {
			throw new Exception("The first name is too short, it must have a minimum of 1 character!");
		}
		else if(firstName.length() > 32) {
			throw new Exception("The first name is too long, it must have a maximum of 32 characters!");
		}
		
		// Check allowed characters
		List<Character> allowedChars = (
			"ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
			"abcdefghijklmnopqrstuvwxyz"
			).chars().mapToObj(c -> (char)c).collect(Collectors.toList());
		for(int i = 0; i < firstName.length(); i++) {
			if(!allowedChars.contains(firstName.charAt(i))) {
				throw new Exception("The first name contains unallowed characters!");
			}
		}
		
		// Valid
		this.firstName = firstName;
	}
	
	
	// Getters/setters: lastName
	public String getLastName() {
		return lastName;
	}
	
	public void setLastName(String lastName) throws Exception {
		// Check length
		if(lastName.length() < 1) {
			throw new Exception("The last name is too short, it must have a minimum of 1 character!");
		}
		else if(lastName.length() > 32) {
			throw new Exception("The last name is too long, it must have a maximum of 32 characters!");
		}
		
		// Check allowed characters
		List<Character> allowedChars = (
			"ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
			"abcdefghijklmnopqrstuvwxyz"
			).chars().mapToObj(c -> (char)c).collect(Collectors.toList());
		for(int i = 0; i < lastName.length(); i++) {
			if(!allowedChars.contains(lastName.charAt(i))) {
				throw new Exception("The last name contains unallowed characters!");
			}
		}
		
		// Valid
		this.lastName = lastName;
	}
	
	
	// Methods: override
	@Override
	public String toString() {
		return getFirstName() + " " + getLastName();
	}
}