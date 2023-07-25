package SAR0083.Homework.models;

import javax.persistence.Column;
import javax.persistence.Embedded;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.Table;
import javax.persistence.Transient;
import javax.persistence.UniqueConstraint;

import com.fasterxml.jackson.annotation.JsonIgnore;

@Entity
@Table(uniqueConstraints = {
	@UniqueConstraint(name = "UniqueEmailAddress", columnNames = {"email_local_part", "email_domain"})
})
public class Account {
	// Properties
	@Id
	@GeneratedValue(strategy = GenerationType.IDENTITY)
	private long id;
	@Embedded
	private EmailAddress emailAddress = new EmailAddress();
	@JsonIgnore
	@Column(nullable = false)
	private String password;
	@JsonIgnore
	@Transient
	private String passwordRepeated;
	
	
	// Constructors
	public Account() {
		super();
	}
	
	public Account(EmailAddress emailAddress) throws Exception {
		super();
		setEmailAddress(emailAddress);
	}
	
	public Account(EmailAddress emailAddress, String password, String passwordRepeated) throws Exception {
		this(emailAddress);
		setPassword(password);
		setPasswordRepeated(passwordRepeated);
	}
	
	
	// Getters/setters: id
	public long getId() {
		return id;
	}
	
	public void setId(long id) {
		this.id = id;
	}
	
	
	// Getters/setters: emailAddress
	public EmailAddress getEmailAddress() {
		return emailAddress;
	}
	
	public void setEmailAddress(EmailAddress emailAddress) {
		this.emailAddress = emailAddress;
	}
	
	
	// Getters/setters: password
	public String getPassword() {
		return password;
	}
	
	public void setPassword(String password) throws Exception {
		this.password = password;
	}
	
	
	// Getters/setters: passwordRepeated
	public String getPasswordRepeated() {
		return passwordRepeated;
	}
	
	public void setPasswordRepeated(String passwordRepeated) throws Exception {
		this.passwordRepeated = passwordRepeated;
	}
	
	
	// Methods: override
	@Override
	public String toString() {
		return "#" + getId() + "; " + getEmailAddress();
	}
}