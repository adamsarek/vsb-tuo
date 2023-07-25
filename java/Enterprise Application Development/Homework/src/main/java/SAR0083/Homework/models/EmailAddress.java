package SAR0083.Homework.models;

import java.util.List;
import java.util.stream.Collectors;

import javax.persistence.Column;
import javax.persistence.Embeddable;

@Embeddable
public class EmailAddress {
	// Properties
	@Column(name = "email_local_part", length = 64, nullable = false)
	private String localPart;
	@Column(name = "email_domain", length = 255, nullable = false)
	private String domain;
	
	
	// Constructors
	public EmailAddress() {
		super();
	}
	
	public EmailAddress(String localPart, String domain) throws Exception {
		super();
		setLocalPart(localPart);
		setDomain(domain);
	}
	
	
	// Getters/setters: localPart
	public String getLocalPart() {
		return localPart;
	}
	
	public void setLocalPart(String localPart) throws Exception {
		// Check length
		if(localPart.length() < 1) {
			throw new Exception("The local part is too short, it must have a minimum of 1 character!");
		}
		else if(localPart.length() > 64) {
			throw new Exception("The local part is too long, it must have a maximum of 64 characters!");
		}
		
		// Check allowed characters
		List<Character> allowedChars = (
			"ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
			"abcdefghijklmnopqrstuvwxyz" +
			"0123456789" +
			"!#$%&'*+-/=?^_`{|}~" +
			"."
			).chars().mapToObj(c -> (char)c).collect(Collectors.toList());
		for(int i = 0; i < localPart.length(); i++) {
			if(!allowedChars.contains(localPart.charAt(i))) {
				throw new Exception("The local part contains unallowed characters!");
			}
		}
		
		// Check dot rules
		if(localPart.charAt(0) == '.') {
			throw new Exception("The first character of the local part must not be a dot!");
		}
		else if(localPart.charAt(localPart.length() - 1) == '.') {
			throw new Exception("The last character of the local part must not be a dot!");
		}
		else if(localPart.indexOf("..") > -1) {
			throw new Exception("The local part must not contain two or more consecutive dots!");
		}
		
		// Valid
		this.localPart = localPart;
	}
	
	
	// Getters/setters: domain
	public String getDomain() {
		return domain;
	}
	
	public void setDomain(String domain) throws Exception {
		// Check length
		if(domain.length() < 4) {
			throw new Exception("The domain is too short, it must have a minimum of 4 characters!");
		}
		else if(domain.length() > 255) {
			throw new Exception("The domain is too long, it must have a maximum of 255 characters!");
		}
		
		// Check allowed characters
		List<Character> allowedChars = (
			"ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
			"abcdefghijklmnopqrstuvwxyz" +
			"0123456789" +
			"-" +
			"."
			).chars().mapToObj(c -> (char)c).collect(Collectors.toList());
		for(int i = 0; i < domain.length(); i++) {
			if(!allowedChars.contains(domain.charAt(i))) {
				throw new Exception("The domain contains unallowed characters!");
			}
		}
		
		// Check dot rules
		if(domain.charAt(0) == '.') {
			throw new Exception("The first character of the domain must not be a dot!");
		}
		else if(domain.charAt(domain.length() - 1) == '.') {
			throw new Exception("The last character of the domain must not be a dot!");
		}
		else if(domain.indexOf(".") <= -1) {
			throw new Exception("The domain must contain a dot!");
		}
		
		// Check domain parts rules
		String[] domainParts = domain.split("\\.");
		for(int i = 0; i < domainParts.length; i++) {
			// Check length
			if(domainParts[i].length() < 1) {
				throw new Exception("The DNS label of the domain is too short, it must have a minimum of 1 character!");
			}
			else if(domainParts[i].length() > 63) {
				throw new Exception("The DNS label of the domain is too long, it must have a maximum of 63 characters!");
			}
			
			// Check hyphen rules
			if(domainParts[i].charAt(0) == '-') {
				throw new Exception("The first character of the DNS label of the domain must not be a hyphen!");
			}
			else if(domainParts[i].charAt(domainParts[i].length() - 1) == '-') {
				throw new Exception("The last character of the DNS label of the domain must not be a hyphen!");
			}
		}
		
		// Check top-level DNS label is not all-numeric characters
		List<Character> allNumericChars = (
			"0123456789"
			).chars().mapToObj(c -> (char)c).collect(Collectors.toList());
		boolean topLevelDomainAllNumericChars = true;
		for(int i = 0; i < domainParts[domainParts.length - 1].length(); i++) {
			if(!allNumericChars.contains(domainParts[domainParts.length - 1].charAt(i))) {
				topLevelDomainAllNumericChars = false;
				break;
			}
		}
		if(topLevelDomainAllNumericChars) {
			throw new Exception("The top-level DNS label of the domain is all-numeric!");
		}
		
		// Valid
		this.domain = domain;
	}
	
	
	// Methods: override
	@Override
	public String toString() {
		if(getLocalPart() != null && getDomain() != null) {
			return getLocalPart() + "@" + getDomain();
		}
		return "";
	}
}