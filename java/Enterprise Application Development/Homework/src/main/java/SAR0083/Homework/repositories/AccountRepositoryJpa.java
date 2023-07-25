package SAR0083.Homework.repositories;

import java.util.List;

import javax.persistence.EntityManager;
import javax.persistence.PersistenceContext;
import javax.transaction.Transactional;

import org.springframework.stereotype.Repository;

import SAR0083.Homework.models.EmailAddress;
import SAR0083.Homework.models.Account;

@Repository
public class AccountRepositoryJpa implements AccountRepository {
	// Properties
	@PersistenceContext
	private EntityManager em;
	
	
	// CRUD: read
	@Override
	public List<Account> getAccounts() {
		return em.createQuery(
			"SELECT account "
			+ "FROM Account account",
			Account.class
		).getResultList();
	}
	
	@Override
	public Account getAccount(long id) {
		return em.find(Account.class, id);
	}
	
	@Override
	public Account getAccount(EmailAddress emailAddress) {
		return em.createQuery(
			"SELECT account "
			+ "FROM Account account "
			+ "WHERE email_local_part = :email_local_part AND email_domain = :email_domain",
			Account.class
		)
		.setParameter("email_local_part", emailAddress.getLocalPart())
		.setParameter("email_domain", emailAddress.getDomain())
		.getResultList()
		.stream()
		.findFirst()
		.orElse(null);
	}
	
	
	// CRUD: create
	@Override
	@Transactional
	public void addAccount(Account account) {
		if(account.getId() == 0) {
			em.persist(account);
		}
	}
	
	
	// CRUD: update
	@Override
	@Transactional
	public void setAccount(Account account) {
		if(account.getId() != 0) {
			em.merge(account);
		}
	}
	
	
	// CRUD: delete
	@Override
	@Transactional
	public void delete(long id) {
		if(id != 0) {
			Account account = getAccount(id);
			em.remove(account);
		}
	}
}