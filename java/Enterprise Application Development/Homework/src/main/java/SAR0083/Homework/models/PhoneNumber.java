package SAR0083.Homework.models;

import java.util.List;
import java.util.stream.Collectors;

import javax.persistence.Column;
import javax.persistence.Embeddable;

@Embeddable
public class PhoneNumber {
	// Properties
	@Column(name = "phone_country_code", length = 3, nullable = false)
	private String countryCode;
	@Column(name = "phone_subscriber_number", length = 12, nullable = false)
	private String subscriberNumber;
	
	
	// Constructors
	public PhoneNumber() {
		super();
	}
	
	public PhoneNumber(String countryCode, String subscriberNumber) throws Exception {
		super();
		setCountryCode(countryCode);
		setSubscriberNumber(subscriberNumber);
	}
	
	
	// Getters/setters: countryCode
	public String getCountryCode() {
		return countryCode;
	}
	
	public void setCountryCode(String countryCode) throws Exception {
		// Check length
		if(countryCode.length() < 1) {
			throw new Exception("The country code is too short, it must have a minimum of 1 digit!");
		}
		else if(countryCode.length() > 3) {
			throw new Exception("The country code is too long, it must have a maximum of 3 digits!");
		}
		
		// Check allowed characters
		List<Character> allowedChars = (
			"0123456789"
			).chars().mapToObj(c -> (char)c).collect(Collectors.toList());
		for(int i = 0; i < countryCode.length(); i++) {
			if(!allowedChars.contains(countryCode.charAt(i))) {
				throw new Exception("The country code contains unallowed characters!");
			}
		}
		
		// Valid
		this.countryCode = countryCode;
	}
	
	
	// Getters/setters: subscriberNumber
	public String getSubscriberNumber() {
		return subscriberNumber;
	}
	
	public void setSubscriberNumber(String subscriberNumber) throws Exception {
		// Check length
		if(subscriberNumber.length() < 4) {
			throw new Exception("The subscriber number is too short, it must have a minimum of 4 digits!");
		}
		else if(subscriberNumber.length() > 12) {
			throw new Exception("The subscriber number is too long, it must have a maximum of 12 digits!");
		}
		
		// Check allowed characters
		List<Character> allowedChars = (
			"0123456789"
			).chars().mapToObj(c -> (char)c).collect(Collectors.toList());
		for(int i = 0; i < subscriberNumber.length(); i++) {
			if(!allowedChars.contains(subscriberNumber.charAt(i))) {
				throw new Exception("The subscriber number contains unallowed characters!");
			}
		}
		
		// Valid
		this.subscriberNumber = subscriberNumber;
	}
	
	
	// Methods: override
	@Override
	public String toString() {
		if(getCountryCode() != null && getSubscriberNumber() != null) {
			return "+" + getCountryCode() + getSubscriberNumber();
		}
		return "";
	}
}