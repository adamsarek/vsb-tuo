package SAR0083.Homework.services;

import java.util.ArrayList;
import java.util.List;
import java.util.stream.Collectors;

import javax.annotation.PostConstruct;
import javax.transaction.Transactional;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.security.core.GrantedAuthority;
import org.springframework.security.core.authority.SimpleGrantedAuthority;
import org.springframework.security.core.userdetails.User;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UserDetailsService;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.stereotype.Service;

import SAR0083.Homework.models.EmailAddress;
import SAR0083.Homework.StringToEmailAddressConverter;
import SAR0083.Homework.models.Account;
import SAR0083.Homework.repositories.AccountRepository;
import SAR0083.Homework.repositories.AccountRepositoryJdbc;
import SAR0083.Homework.repositories.AccountRepositoryJpa;

@Service
@Transactional
public class AccountService implements UserDetailsService {
	// Properties
	private AccountRepository accountRepository;
	@Autowired
	private AccountRepositoryJpa accountRepositoryJpa;
	@Autowired
	private AccountRepositoryJdbc accountRepositoryJdbc;
	@Value("${repository}")
	private String repository;
	
	
	// Repository
	@PostConstruct
	public void setRepository() {
		switch(repository) {
			case "jdbc":
				this.accountRepository = accountRepositoryJdbc;
				break;
			default:
				this.accountRepository = accountRepositoryJpa;
				break;
		}
	}
	
	
	// Security
	public boolean isPasswordValid(String password) throws Exception {
		// Check length
		if(password.length() < 8) {
			throw new Exception("The password is too short, it must have a minimum of 8 characters!");
		}
		else if(password.length() > 32) {
			throw new Exception("The password is too long, it must have a maximum of 32 characters!");
		}
		
		// Check allowed characters
		List<Character> allowedChars = (
			"ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
			"abcdefghijklmnopqrstuvwxyz" +
			"0123456789" +
			"!#$%&'*+-/=?^_`{|}~" +
			"."
			).chars().mapToObj(c -> (char)c).collect(Collectors.toList());
		for(int i = 0; i < password.length(); i++) {
			if(!allowedChars.contains(password.charAt(i))) {
				throw new Exception("The password contains unallowed characters!");
			}
		}
		
		// Valid
		return true;
	}
	
	public UserDetails loadUserByUsername(String email) throws UsernameNotFoundException {
		EmailAddress emailAddress = new StringToEmailAddressConverter().convert(email);
		Account account = accountRepository.getAccount(emailAddress);
		
		if(account == null) {
			throw new UsernameNotFoundException("No account found with email: " + email);
		}
		
		boolean enabled = true;
		boolean accountNonExpired = true;
		boolean credentialsNonExpired = true;
		boolean accountNonLocked = true;
		
		List<GrantedAuthority> authorities = new ArrayList<>();
		authorities.add(new SimpleGrantedAuthority("ROLE_USER"));
		
		return new User(
			account.getEmailAddress().toString(),
			account.getPassword(),
			enabled,
			accountNonExpired,
			credentialsNonExpired,
			accountNonLocked,
			authorities
		);
	}
	
	
	// CRUD: read
	public List<Account> getAccounts() {
		return accountRepository.getAccounts();
	}
	
	public Account getAccount(long id) {
		return accountRepository.getAccount(id);
	}
	
	public Account getAccount(EmailAddress emailAddress) {
		return accountRepository.getAccount(emailAddress);
	}
	
	
	// CRUD: create
	public void addAccount(Account account) throws Exception {
		// Check if account exists
		if(getAccount(account.getEmailAddress()) != null) {
			throw new Exception("The account already exists!");
		}
		
		// Check password is valid
		isPasswordValid(account.getPassword());
		
		// Check passwords match
		if(!account.getPassword().equals(account.getPasswordRepeated())) {
			throw new Exception("The passwords do not match!");
		}
		
		// Valid
		account.setPassword(new BCryptPasswordEncoder().encode(account.getPassword()));
		accountRepository.addAccount(account);
	}
	
	
	// CRUD: update
	public void setAccount(Account account) {
		accountRepository.setAccount(account);
	}
	
	
	// CRUD: delete
	public void delete(long id) {
		accountRepository.delete(id);
	}
}