package SAR0083.Homework;

import java.time.LocalDate;
import java.util.ArrayList;
import java.util.List;

import javax.transaction.Transactional;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.boot.context.event.ApplicationReadyEvent;
import org.springframework.context.annotation.Configuration;
import org.springframework.context.event.EventListener;

import SAR0083.Homework.models.Account;
import SAR0083.Homework.models.Car;
import SAR0083.Homework.models.Customer;
import SAR0083.Homework.models.EmailAddress;
import SAR0083.Homework.models.FullName;
import SAR0083.Homework.models.Lease;
import SAR0083.Homework.models.PhoneNumber;
import SAR0083.Homework.models.Truck;
import SAR0083.Homework.models.Trunk;
import SAR0083.Homework.services.AccountService;
import SAR0083.Homework.services.CustomerService;
import SAR0083.Homework.services.LeaseService;
import SAR0083.Homework.services.VehicleService;

@Configuration
public class TestData {
	// Properties
	@Autowired
	private AccountService accountService;
	@Autowired
	private VehicleService vehicleService;
	@Autowired
	private CustomerService customerService;
	@Autowired
	private LeaseService leaseService;
	@Value("${saveTestData}")
	private boolean saveTestData;
	
	
	// Save test data
	@EventListener(ApplicationReadyEvent.class)
	@Transactional
	public void saveTestData() throws Exception {
		if(saveTestData) {
			addAccounts();
			addVehicles();
			addCustomers();
			addLeases();
		}
	}
	
	public void addAccounts() throws Exception {
		List<Account> accounts = new ArrayList<>();
		accounts.add(new Account(new EmailAddress("test", "email.com"), "test1234", "test1234"));
		accounts.add(new Account(new EmailAddress("user", "gmail.com"), "user5678", "user5678"));
		for(Account account : accounts) {
			accountService.addAccount(account);
		}
	}
	
	public void addVehicles() throws Exception {
		List<Car> cars = new ArrayList<>();
		cars.add(new Car("Skoda", "Octavia", (short) 1996, 1459f, Trunk.Front));
		cars.add(new Car("Ford", "Focus", (short) 1998, 1471f, Trunk.Rear));
		cars.add(new Car("Tesla", "Model X", (short) 2015, 2301f, Trunk.Both));
		for(Car car : cars) {
			vehicleService.setCar(car);
		}
		
		List<Truck> trucks = new ArrayList<>();
		trucks.add(new Truck("PACCAR", "W990", (short) 2000, 1179.34f, (short) 1));
		trucks.add(new Truck("Freightliner", "Cascadia", (short) 2008, 27488.16f, (short) 3));
		trucks.add(new Truck("Volvo", "FH16", (short) 2012, 56000f, (short) 4));
		for(Truck truck : trucks) {
			vehicleService.setTruck(truck);
		}
	}
	
	public void addCustomers() throws Exception {
		List<Customer> customers = new ArrayList<>();
		customers.add(new Customer(new FullName("Adam", "Brown"), LocalDate.of(2000, 6, 9), new EmailAddress("adam.brown", "gmail.com"), new PhoneNumber("1", "987654321")));
		customers.add(new Customer(new FullName("Chad", "Giga"), LocalDate.of(1985, 3, 20), new EmailAddress("chad.giga", "gmail.com"), new PhoneNumber("48", "101202303")));
		customers.add(new Customer(new FullName("Dereck", "Tucson"), LocalDate.of(1962, 12, 1), new EmailAddress("dereck", "tucson.com"), new PhoneNumber("420", "123456789")));
		for(Customer customer : customers) {
			customerService.setCustomer(customer);
		}
	}
	
	public void addLeases() throws Exception {
		List<Lease> leases = new ArrayList<>();
		leases.add(new Lease(LocalDate.of(2001, 2, 3), LocalDate.of(2002, 3, 4), true, vehicleService.getVehicle(1), customerService.getCustomer(1)));
		leases.add(new Lease(LocalDate.of(2005, 5, 20), LocalDate.of(2006, 1, 12), false, vehicleService.getVehicle(1), customerService.getCustomer(2)));
		leases.add(new Lease(LocalDate.of(2007, 11, 13), LocalDate.of(2009, 11, 24), true, vehicleService.getVehicle(2), customerService.getCustomer(3)));
		for(Lease lease : leases) {
			leaseService.setLease(lease);
		}
	}
}
