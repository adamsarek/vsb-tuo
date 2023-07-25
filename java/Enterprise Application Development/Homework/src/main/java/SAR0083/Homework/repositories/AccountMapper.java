package SAR0083.Homework.repositories;

import java.sql.ResultSet;
import java.sql.SQLException;

import org.springframework.jdbc.core.RowMapper;

import SAR0083.Homework.models.Account;
import SAR0083.Homework.models.EmailAddress;

public class AccountMapper implements RowMapper<Account> {
	// Methods: override
	@Override
	public Account mapRow(ResultSet rs, int rowNum) throws SQLException {
		Account account = new Account();
		
		try {
			account.setId(rs.getLong("id"));
			account.setPassword(rs.getString("password"));
			
			EmailAddress emailAddress = new EmailAddress();
			emailAddress.setLocalPart(rs.getString("email_local_part"));
			emailAddress.setDomain(rs.getString("email_domain"));
			account.setEmailAddress(emailAddress);
		} catch(Exception e) {
			e.printStackTrace();
		}
		
		return account;
	}
}