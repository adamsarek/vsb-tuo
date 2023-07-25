package SAR0083.Homework.repositories;

import java.util.List;

import javax.annotation.PostConstruct;
import javax.sql.DataSource;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.stereotype.Repository;

import SAR0083.Homework.models.Account;
import SAR0083.Homework.models.EmailAddress;

@Repository
public class AccountRepositoryJdbc implements AccountRepository {
	// Properties
	@Autowired
	private DataSource dataSource;
	private JdbcTemplate jdbcTemplate;
	
	
	// Constructor
	public AccountRepositoryJdbc() {
		
	}
	
	
	// Methods: postConstruct
	@PostConstruct
	public void init() {
		jdbcTemplate = new JdbcTemplate(dataSource);
	}
	
	
	// CRUD: read
	@Override
	public List<Account> getAccounts() {
		return jdbcTemplate.query(
			"SELECT * "
			+ "FROM Account",
			new AccountMapper()
		);
	}
	
	@Override
	public Account getAccount(long id) {
		return jdbcTemplate.queryForObject(
			"SELECT * "
			+ "FROM Account "
			+ "WHERE id = ?",
			new AccountMapper(),
			id
		);
	}
	
	@Override
	public Account getAccount(EmailAddress emailAddress) {
		try {
			return jdbcTemplate.queryForObject(
				"SELECT * "
				+ "FROM Account "
				+ "WHERE email_local_part = ? AND email_domain = ?",
				new AccountMapper(),
				emailAddress.getLocalPart(),
				emailAddress.getDomain()
			);
		} catch(Exception e) {}
		
		return null;
	}
	
	
	// CRUD: create
	@Override
	public void addAccount(Account account) {
		if(account.getId() == 0) {
			jdbcTemplate.update(
				"INSERT INTO Account ("
					+ "email_local_part, "
					+ "email_domain, "
					+ "password"
				+ ") VALUES (?, ?, ?)",
				account.getEmailAddress().getLocalPart(),
				account.getEmailAddress().getDomain(),
				account.getPassword()
			);
		}
	}
	
	
	// CRUD: update
	@Override
	public void setAccount(Account account) {
		if(account.getId() != 0) {
			jdbcTemplate.update(
				"UPDATE Account "
				+ "SET "
					+ "email_local_part = ?, "
					+ "email_domain = ?, "
					+ "password = ? "
				+ "WHERE id = ?",
				account.getEmailAddress().getLocalPart(),
				account.getEmailAddress().getDomain(),
				account.getPassword(),
				account.getId()
			);
		}
	}
	
	
	// CRUD: delete
	public void delete(long id) {
		jdbcTemplate.update(
			"DELETE FROM Account "
			+ "WHERE id = ?",
			id
		);
	}
}