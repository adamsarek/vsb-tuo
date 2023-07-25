package SAR0083.Homework.controllers;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.server.ResponseStatusException;

import SAR0083.Homework.models.Account;
import SAR0083.Homework.models.Car;
import SAR0083.Homework.models.Customer;
import SAR0083.Homework.models.Lease;
import SAR0083.Homework.models.Truck;
import SAR0083.Homework.models.Vehicle;
import SAR0083.Homework.services.AccountService;
import SAR0083.Homework.services.CustomerService;
import SAR0083.Homework.services.LeaseService;
import SAR0083.Homework.services.VehicleService;

@RestController
@RequestMapping(path = "/api", produces = MediaType.APPLICATION_JSON_VALUE)
public class RestApiController {
	// Properties
	@Autowired
	private AccountService accountService;
	@Autowired
	private VehicleService vehicleService;
	@Autowired
	private CustomerService customerService;
	@Autowired
	private LeaseService leaseService;
	
	
	// Data: account
	// CRUD: read
	@GetMapping(path = "/accounts")
	public List<Account> getAccounts() {
		return accountService.getAccounts();
	}
	
	@GetMapping(path = "/accounts/{id}")
	public Account getAccount(@PathVariable long id) {
		if(accountService.getAccount(id) == null) {
			throw new ResponseStatusException(HttpStatus.NOT_FOUND);
		}
		
		return accountService.getAccount(id);
	}
	
	
	// Data: vehicle
	// CRUD: read
	@GetMapping(path = "/vehicles")
	public List<Vehicle> getVehicles() {
		return vehicleService.getVehicles();
	}
	
	@GetMapping(path = "/vehicles/{id}")
	public Vehicle getVehicle(@PathVariable long id) {
		if(vehicleService.getVehicle(id) == null) {
			throw new ResponseStatusException(HttpStatus.NOT_FOUND);
		}
		
		return vehicleService.getVehicle(id);
	}
	
	// CRUD: delete
	@DeleteMapping(path = "/vehicles/{id}")
	public void deleteVehicle(@PathVariable long id) {
		if(vehicleService.getVehicle(id) == null) {
			throw new ResponseStatusException(HttpStatus.NOT_FOUND);
		}
		
		vehicleService.delete(id);
	}
	
	
	// Data: car
	// CRUD: read
	@GetMapping(path = "/cars")
	public List<Car> getCars() {
		return vehicleService.getCars();
	}
	
	@GetMapping(path = "/cars/{id}")
	public Car getCar(@PathVariable long id) {
		if(vehicleService.getCar(id) == null) {
			throw new ResponseStatusException(HttpStatus.NOT_FOUND);
		}
		
		return vehicleService.getCar(id);
	}
	
	// CRUD: create
	@PostMapping(path = "/cars", consumes = MediaType.APPLICATION_JSON_VALUE)
	public void setCar(@RequestBody Car car) {
		vehicleService.setCar(car);
	}
	
	// CRUD: update
	@PutMapping(path = "/cars/{id}", consumes = MediaType.APPLICATION_JSON_VALUE)
	public void setCar(@PathVariable long id, @RequestBody Car car) {
		if(vehicleService.getCar(id) == null) {
			throw new ResponseStatusException(HttpStatus.NOT_FOUND);
		}
		
		car.setId(id);
		vehicleService.setCar(car);
	}
	
	// CRUD: delete
	@DeleteMapping(path = "/cars/{id}")
	public void deleteCar(@PathVariable long id) {
		if(vehicleService.getCar(id) == null) {
			throw new ResponseStatusException(HttpStatus.NOT_FOUND);
		}
		
		vehicleService.delete(id);
	}
	
	
	// Data: truck
	// CRUD: read
	@GetMapping(path = "/trucks")
	public List<Truck> getTrucks() {
		return vehicleService.getTrucks();
	}
	
	@GetMapping(path = "/trucks/{id}")
	public Truck getTruck(@PathVariable long id) {
		if(vehicleService.getTruck(id) == null) {
			throw new ResponseStatusException(HttpStatus.NOT_FOUND);
		}
		
		return vehicleService.getTruck(id);
	}
	
	// CRUD: create
	@PostMapping(path = "/trucks", consumes = MediaType.APPLICATION_JSON_VALUE)
	public void setTruck(@RequestBody Truck car) {
		vehicleService.setTruck(car);
	}
	
	// CRUD: update
	@PutMapping(path = "/trucks/{id}", consumes = MediaType.APPLICATION_JSON_VALUE)
	public void setTruck(@PathVariable long id, @RequestBody Truck truck) {
		if(vehicleService.getTruck(id) == null) {
			throw new ResponseStatusException(HttpStatus.NOT_FOUND);
		}
		
		truck.setId(id);
		vehicleService.setTruck(truck);
	}
	
	// CRUD: delete
	@DeleteMapping(path = "/trucks/{id}")
	public void deleteTruck(@PathVariable long id) {
		if(vehicleService.getTruck(id) == null) {
			throw new ResponseStatusException(HttpStatus.NOT_FOUND);
		}
		
		vehicleService.delete(id);
	}
	
	
	// Data: customer
	// CRUD: read
	@GetMapping(path = "/customers")
	public List<Customer> getCustomers() {
		return customerService.getCustomers();
	}
	
	@GetMapping(path = "/customers/{id}")
	public Customer getCustomer(@PathVariable long id) {
		if(customerService.getCustomer(id) == null) {
			throw new ResponseStatusException(HttpStatus.NOT_FOUND);
		}
		
		return customerService.getCustomer(id);
	}
	
	// CRUD: create
	@PostMapping(path = "/customers", consumes = MediaType.APPLICATION_JSON_VALUE)
	public void setCustomer(@RequestBody Customer car) {
		customerService.setCustomer(car);
	}
	
	// CRUD: update
	@PutMapping(path = "/customers/{id}", consumes = MediaType.APPLICATION_JSON_VALUE)
	public void setCustomer(@PathVariable long id, @RequestBody Customer customer) {
		if(customerService.getCustomer(id) == null) {
			throw new ResponseStatusException(HttpStatus.NOT_FOUND);
		}
		
		customer.setId(id);
		customerService.setCustomer(customer);
	}
	
	// CRUD: delete
	@DeleteMapping(path = "/customers/{id}")
	public void deleteCustomer(@PathVariable long id) {
		if(customerService.getCustomer(id) == null) {
			throw new ResponseStatusException(HttpStatus.NOT_FOUND);
		}
		
		customerService.delete(id);
	}
	
	
	// Data: lease
	// CRUD: read
	@GetMapping(path = "/leases")
	public List<Lease> getLeases() {
		return leaseService.getLeases();
	}
	
	@GetMapping(path = "/leases/{id}")
	public Lease getLease(@PathVariable long id) {
		if(leaseService.getLease(id) == null) {
			throw new ResponseStatusException(HttpStatus.NOT_FOUND);
		}
		
		return leaseService.getLease(id);
	}
	
	// CRUD: create
	@PostMapping(path = "/leases", consumes = MediaType.APPLICATION_JSON_VALUE)
	public void setLease(@RequestBody Lease car) {
		leaseService.setLease(car);
	}
	
	// CRUD: update
	@PutMapping(path = "/leases/{id}", consumes = MediaType.APPLICATION_JSON_VALUE)
	public void setLease(@PathVariable long id, @RequestBody Lease lease) {
		if(leaseService.getLease(id) == null) {
			throw new ResponseStatusException(HttpStatus.NOT_FOUND);
		}
		
		lease.setId(id);
		leaseService.setLease(lease);
	}
	
	// CRUD: delete
	@DeleteMapping(path = "/leases/{id}")
	public void deleteLease(@PathVariable long id) {
		if(leaseService.getLease(id) == null) {
			throw new ResponseStatusException(HttpStatus.NOT_FOUND);
		}
		
		leaseService.delete(id);
	}
}