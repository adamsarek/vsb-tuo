package SAR0083.Homework.repositories;

import java.sql.ResultSet;
import java.sql.SQLException;

import org.springframework.jdbc.core.RowMapper;

import SAR0083.Homework.models.Customer;
import SAR0083.Homework.models.EmailAddress;
import SAR0083.Homework.models.PhoneNumber;

public class CustomerMapper implements RowMapper<Customer> {
	// Methods: override
	@Override
	public Customer mapRow(ResultSet rs, int rowNum) throws SQLException {
		Customer customer = new Customer();
		
		try {
			customer.setId(rs.getLong("id"));
			customer.setFirstName(rs.getString("first_name"));
			customer.setLastName(rs.getString("last_name"));
			customer.setBirthDate(rs.getDate("birth_date").toLocalDate());
			
			EmailAddress emailAddress = new EmailAddress();
			emailAddress.setLocalPart(rs.getString("email_local_part"));
			emailAddress.setDomain(rs.getString("email_domain"));
			customer.setEmailAddress(emailAddress);
			
			PhoneNumber phoneNumber = new PhoneNumber();
			phoneNumber.setCountryCode(rs.getString("phone_country_code"));
			phoneNumber.setSubscriberNumber(rs.getString("phone_subscriber_number"));
			customer.setPhoneNumber(phoneNumber);
		} catch(Exception e) {
			e.printStackTrace();
		}
		
		return customer;
	}
}