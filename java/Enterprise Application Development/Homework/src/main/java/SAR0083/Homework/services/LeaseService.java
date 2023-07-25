package SAR0083.Homework.services;

import java.util.List;

import javax.annotation.PostConstruct;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Service;

import SAR0083.Homework.models.Lease;
import SAR0083.Homework.repositories.LeaseRepository;
import SAR0083.Homework.repositories.LeaseRepositoryJdbc;
import SAR0083.Homework.repositories.LeaseRepositoryJpa;

@Service
public class LeaseService {
	// Properties
	private LeaseRepository leaseRepository;
	@Autowired
	private LeaseRepositoryJpa leaseRepositoryJpa;
	@Autowired
	private LeaseRepositoryJdbc leaseRepositoryJdbc;
	@Value("${repository}")
	private String repository;
	
	
	// Repository
	@PostConstruct
	public void setRepository() {
		switch(repository) {
			case "jdbc":
				this.leaseRepository = leaseRepositoryJdbc;
				break;
			default:
				this.leaseRepository = leaseRepositoryJpa;
				break;
		}
	}
	
	
	// CRUD: read
	public List<Lease> getLeases() {
		return leaseRepository.getLeases();
	}
	
	public Lease getLease(long id) {
		return leaseRepository.getLease(id);
	}
	
	
	// CRUD: create + update
	public void setLease(Lease lease) {
		leaseRepository.setLease(lease);
	}
	
	
	// CRUD: delete
	public void delete(long id) {
		leaseRepository.delete(id);
	}
}