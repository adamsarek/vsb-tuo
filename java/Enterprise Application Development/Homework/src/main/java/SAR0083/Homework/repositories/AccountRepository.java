package SAR0083.Homework.repositories;

import java.util.List;

import org.springframework.stereotype.Repository;

import SAR0083.Homework.models.EmailAddress;
import SAR0083.Homework.models.Account;

@Repository
public interface AccountRepository {
	// CRUD: read
	List<Account> getAccounts();
	
	Account getAccount(long id);
	
	Account getAccount(EmailAddress emailAddress);
	
	
	// CRUD: create + update
	void addAccount(Account account);
	
	void setAccount(Account account);
	
	
	// CRUD: delete
	void delete(long id);
}